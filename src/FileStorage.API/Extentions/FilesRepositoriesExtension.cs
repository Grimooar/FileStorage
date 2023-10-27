using FileStorage.Domain.Models;
using FileStorage.Infrastructure.Context;
using Kirel.Repositories.Core.Interfaces;
using Kirel.Repositories.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace FileStorage.API.Extentions;

public static class FilesRepositoriesExtension
{
    /// <summary>
    /// </summary>
    /// <param name="services"> Collection services </param>
    public static void AddFilesRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IKirelGenericEntityRepository<int, FileData>,
                KirelGenericEntityFrameworkRepository<int, FileData, FileDbContext>>();
        services
            .AddScoped<IKirelGenericEntityRepository<int, User>,
                KirelGenericEntityFrameworkRepository<int, User, FileDbContext>>();
            services.AddIdentity<User, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<FileDbContext>();
            services
                .AddScoped<IKirelGenericEntityRepository<int, FilePart>,
                    KirelGenericEntityFrameworkRepository<int, FilePart, FileDbContext>>();
        
    }
}