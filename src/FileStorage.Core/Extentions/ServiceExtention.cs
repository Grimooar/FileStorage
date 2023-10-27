using FileStorage.Core.Service;
using Microsoft.Extensions.DependencyInjection;

namespace FileStorage.Core.Extentions;

public static  class ServiceExtention
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<FileService>();
        services.AddScoped<AuthService>();
    }   
}