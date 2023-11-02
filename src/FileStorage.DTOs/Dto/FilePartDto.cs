using FileStorage.Domain.Models;
using File = FileStorage.Domain.Models.File;

namespace FileStorage.DTOs.Dto;

public class FilePartDto
{
    public int Id { get; set; }
    public byte[] Data { get; set; }
    public int PartNumber { get; set; }
    public int FileDataId { get; set; } 
    public virtual File File { get; set; }
    public DateTime Created { get; set; }
}