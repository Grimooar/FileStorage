using FileStorage.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Context;

public class FileDbContext : DbContext
{
    public FileDbContext(DbContextOptions<FileDbContext> options) : base(options)
    {
        
    }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<FileData>().HasKey(f => f.Id);
        modelBuilder.Entity<Folder>().HasKey(f => f.Id);
        modelBuilder.Entity<FolderFile>().HasKey(ff => ff.Id);

        // Определяем связи между таблицами
        modelBuilder.Entity<User>()
            .HasMany(u => u.Files)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId);

        modelBuilder.Entity<Folder>()
            .HasMany(f => f.FolderFiles)
            .WithOne(ff => ff.Folder)
            .HasForeignKey(ff => ff.FolderId);

        modelBuilder.Entity<FolderFile>()
            .HasOne(ff => ff.FileData)
            .WithMany()
            .HasForeignKey(ff => ff.FileId);
    }
    
    private DbSet<FileData>? Files { get; set; }
    private DbSet<Folder>? Folders { get; set; }
    private DbSet<FolderFile>? FolderFiles { get; set; }
    private DbSet<FilePart> FileParts { get; set; }
}