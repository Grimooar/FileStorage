using FileStorage.Domain.Models;

namespace FileStorage.DTOs.Dto;

public class FilePartDto
{
    public int Id { get; set; }
    public byte[] Data { get; set; }
    public int PartNumber { get; set; }
    public int FileDataId { get; set; } 
    public virtual FileData FileData { get; set; }
    public DateTime Created { get; set; }
}