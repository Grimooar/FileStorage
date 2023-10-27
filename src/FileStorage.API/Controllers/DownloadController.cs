using FileStorage.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Authorize]
    [Route("api/download")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly FileService _fileService;

        public DownloadController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> DownloadFile(int fileId)
        {
            var fileDataDto = await _fileService.DownloadFile(fileId);

            if (fileDataDto == null)
            {
                return NotFound();
            }

            // Создать массив байт из объединенных данных
            var combinedData = new byte[fileDataDto.FileParts.Sum(fp => fp.Data.Length)];
            int currentIndex = 0;

            foreach (var part in fileDataDto.FileParts.OrderBy(fp => fp.PartNumber))
            {
                Array.Copy(part.Data, 0, combinedData, currentIndex, part.Data.Length);
                currentIndex += part.Data.Length;
            }

            // Создать поток данных из объединенных данных
            var combinedStream = new MemoryStream(combinedData);

            // Вернуть объединенный файл как результат
            return File(combinedStream, fileDataDto.ContentType, fileDataDto.FileName);
        }
    }
}