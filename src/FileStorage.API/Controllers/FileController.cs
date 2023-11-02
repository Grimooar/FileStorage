using System.Security.Claims;
using FileStorage.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Authorize]
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileService _fileService;

        public FileController(FileService fileService)
        {
            _fileService = fileService;
        }
        
        [HttpPost("upload")]
        [RequestSizeLimit(Int64.MaxValue)]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile fileDataDto, int userId)
        { 
            userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            if (fileDataDto == null || fileDataDto.Length == 0)
            {
                return BadRequest("Invalid file data.");
            }

            await _fileService.UploadFile(fileDataDto, userId);

            return Ok("File Added");
        }
        
        [HttpGet("{fileId}")]
        public async Task<IActionResult> DownloadFile(int fileId)
        {
            var combinedData = await _fileService.CombineFileParts(fileId);

            if (combinedData == null)
            {
                return NotFound();
            }
            
            var combinedStream = new MemoryStream(combinedData);
            
            return File(combinedStream, "application/octet-stream", "combined_file.ext");
        }
        
        [HttpDelete("{fileId}")]
        public async Task<IActionResult> DeleteFile(int fileId)
        {
            var file = await _fileService.GetFileById(fileId);

            if (file == null)
            {
                return NotFound();
            }

            await _fileService.DeleteFile(fileId);
            return NoContent();
        }

        private async Task<byte[]> GetFileData(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
