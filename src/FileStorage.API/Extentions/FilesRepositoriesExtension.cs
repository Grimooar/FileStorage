using FileStorage.Domain.Models;
using FileStorage.Infrastructure.Context;
using Kirel.Repositories.Core.Interfaces;
using Kirel.Repositories.EntityFramework;
using Microsoft.AspNetCore.Identity;
using File = FileStorage.Domain.Models.File;

namespace FileStorage.API.Extentions;

public static class FilesRepositoriesExtension
{
    /// <summary>
    /// </summary>
    /// <param name="services"> Collection services </param>
    public static void AddFilesRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IKirelGenericEntityRepository<int, File>,
                KirelGenericEntityFrameworkRepository<int, File, FileDbContext>>();
        services
            .AddScoped<IKirelGenericEntityRepository<int, User>,
                KirelGenericEntityFrameworkRepository<int, User, FileDbContext>>();
            services.AddIdentity<User, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<FileDbContext>();
            services
                .AddScoped<IKirelGenericEntityRepository<int, FileDataPart>,
                    KirelGenericEntityFrameworkRepository<int, FileDataPart, FileDbContext>>();
        
    }
}