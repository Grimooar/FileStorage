using Kirel.Repositories.Core.Interfaces;

namespace FileStorage.Domain.Models
{
    public class FileDataPart : IKeyEntity<int>, ICreatedAtTrackedEntity
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public int PartNumber { get; set; }
        public int FileDataId { get; set; } 
        public virtual File File { get; set; }
        public DateTime Created { get; set; }
    }
}