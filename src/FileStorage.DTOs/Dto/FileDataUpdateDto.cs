namespace FileStorage.DTOs.Dto;

public class FileDataUpdateDto
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Data { get; set; }
    public int UserId { get; set; }
}
