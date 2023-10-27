using Microsoft.Extensions.DependencyInjection;

namespace FileStorage.Infrastructure.Context;

public class IdentityDbInitialize
{
    /// <summary>
    /// Initializes the film database and creates tables if they do not exist.
    /// </summary>
    /// <param name="serviceProvider"> The service provider to retrieve the database context. </param>
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<IdentityDbContext>();
        context.Database.EnsureCreated();
    }
}