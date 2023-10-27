using Kirel.Repositories.Core.Interfaces;

namespace FileStorage.Domain.Models;

public class FolderFile : ICreatedAtTrackedEntity, IKeyEntity<int>
{
    public int Id { get; set; }
    public int FolderId { get; set; }
    public int FileId { get; set; }
    public virtual Folder Folder { get; set; }
    public virtual FileData FileData { get; set; }
    public DateTime Created { get; set; }
}