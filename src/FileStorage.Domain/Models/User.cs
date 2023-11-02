using Kirel.Repositories.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FileStorage.Domain.Models;

public class User : IdentityUser<int>, ICreatedAtTrackedEntity, IKeyEntity<int>
{
    public override int Id { get; set; }
    public override string? UserName { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public new string? Email { get; set; }
    public DateTime Created { get; set; }
    public virtual ICollection<File> Files { get; set; }
}
