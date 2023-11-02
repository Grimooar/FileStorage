using Kirel.Repositories.Core.Interfaces;
using System.Collections.Generic;

namespace FileStorage.Domain.Models
{
    public class File : ICreatedAtTrackedEntity, IKeyEntity<int>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public int UserId { get; set; }
        public byte[] Data { get; set; }
        public virtual User User { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<FileDataPart> FileParts { get; set; }
    }
}