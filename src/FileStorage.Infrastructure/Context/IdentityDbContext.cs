using FileStorage.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Context;

public class IdentityDbContext : DbContext
{
   
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<User>().HasKey(b => b.Id);
        }
        private DbSet<User>? User { get; set; }
    
}