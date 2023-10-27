using Kirel.Repositories.Core.Interfaces;

namespace FileStorage.Domain.Models;

public class Folder : ICreatedAtTrackedEntity, IKeyEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public List<FolderFile> FolderFiles { get; set; }
    public DateTime Created { get; set; }
}