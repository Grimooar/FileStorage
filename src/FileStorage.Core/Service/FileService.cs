using AutoMapper;
using FileStorage.Domain.Models;
using FileStorage.DTOs.Dto;
using Kirel.Repositories.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using File = FileStorage.Domain.Models.File;

namespace FileStorage.Core.Service
{
    public class FileService
    {
        private readonly IKirelGenericEntityRepository<int, File> _fileRepository;
        private readonly IKirelGenericEntityRepository<int, FileDataPart> _filePartRepository;
        private readonly IKirelGenericEntityRepository<int, User> _userRepository;
        private readonly IMapper _mapper;

        public FileService(IKirelGenericEntityRepository<int, File> fileRepository, IMapper mapper,
            IKirelGenericEntityRepository<int, User> userRepository,
            IKirelGenericEntityRepository<int, FileDataPart> filePartRepository)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _filePartRepository = filePartRepository;
        }

        public async Task UploadFile(IFormFile fileData, int userId)
        {
            if (fileData == null || fileData.Length == 0)
            {
                throw new ArgumentException("Invalid file data.");
            }

            var buffer = new byte[262144]; 

            
            using (var stream = fileData.OpenReadStream())
            {
                int partNumber = 1; 

                while (true)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    var filePart = new FileDataPart
                    {
                        Data = new byte[bytesRead],
                        PartNumber = partNumber,
                        FileDataId = 0 
                    };

                    Array.Copy(buffer, filePart.Data, bytesRead);

                    var savedPart = await _filePartRepository.Insert(filePart);

                    partNumber++;
                }
            }
        }
        public async Task<byte[]> CombineFileParts(int fileId)
        {
            var fileDataDto = await DownloadFile(fileId);

            if (fileDataDto == null)
            {
                return null; // или бросьте исключение, если необходимо
            }

            var combinedData = new byte[fileDataDto.FileParts.Sum(fp => fp.Data.Length)];
            int currentIndex = 0;

            foreach (var part in fileDataDto.FileParts.OrderBy(fp => fp.PartNumber))
            {
                Array.Copy(part.Data, 0, combinedData, currentIndex, part.Data.Length);
                currentIndex += part.Data.Length;
            }

            return combinedData;
        }

        public async Task<FileDataDto> GetFileById(int fileId)
        {
            var file = await _fileRepository.GetById(fileId);
            if (file == null)
            {
                throw new FileNotFoundException($"File with ID {fileId} was not found in the database.");
            }

            // Преобразование FileModel в FileDataDto
            return _mapper.Map<FileDataDto>(file);
        }
        
        public async Task<List<FileDataPart>> GetFilePartsByFileId(int fileId)
        {
            var parts = await _filePartRepository.GetList(
                fp => fp.FileDataId == fileId,
                null, // Если не требуется сортировка
                null, // Если не требуется включение дополнительных данных
                0,    // Если не требуется пагинация
                0);   // Если не требуется пагинация
            return parts.ToList();
        }

        public async Task<FileDataDto> DownloadFile(int fileId)
        {
            var file = await GetFileById(fileId);

            if (file == null)
            {
                return null;
            }

            var fileParts = await GetFilePartsByFileId(fileId);

            if (fileParts == null || fileParts.Count == 0)
            {
                return null;
            }

            var combinedData = new byte[fileParts.Sum(fp => fp.Data.Length)];
            int currentIndex = 0;

            foreach (var part in fileParts.OrderBy(fp => fp.PartNumber))
            {
                Array.Copy(part.Data, 0, combinedData, currentIndex, part.Data.Length);
                currentIndex += part.Data.Length;
            }

            file.Data = combinedData; 

            return _mapper.Map<FileDataDto>(file);
        }





public async Task<List<FileDataDto>> SearchFiles(string fileName)
        {
            // Поиск файлов в базе данных по имени файла.
            var existingFiles = await _fileRepository.GetList(
                f => f.FileName != null && f.FileName.Contains(fileName),
                includes: q => q.Include(f => f.User)
            );

            if (existingFiles == null || !existingFiles.Any())
            {
                throw new FileNotFoundException($"File with the name '{fileName}' was not found in the database.");
            }

            // Преобразование сущностей файлов в DTO
            var fileDtos = _mapper.Map<List<FileDataDto>>(existingFiles);

            return fileDtos;
        }

        public async Task DeleteFile(int fileId)
        {
            await _fileRepository.Delete(fileId);
        }
    }
}
