using FileStorage.Domain.Models;
using Microsoft.EntityFrameworkCore;
using File = FileStorage.Domain.Models.File;

namespace FileStorage.Infrastructure.Context;

public class FileDbContext : DbContext
{
    public FileDbContext(DbContextOptions<FileDbContext> options) : base(options)
    {
        
    }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<File>().HasKey(f => f.Id);
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
            .HasOne(ff => ff.File)
            .WithMany()
            .HasForeignKey(ff => ff.FileId);
    }
    
    private DbSet<File>? Files { get; set; }
    private DbSet<Folder>? Folders { get; set; }
    private DbSet<FolderFile>? FolderFiles { get; set; }
    private DbSet<FileDataPart> FileParts { get; set; }
}