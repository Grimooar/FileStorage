using FileStorage.Domain.Models;

namespace FileStorage.DTOs.Dto;

public class FileDataDto
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public DateTime Created { get; set; }
    public virtual ICollection<FilePart> FileParts { get; set; }
}
