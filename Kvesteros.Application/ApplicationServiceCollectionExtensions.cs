using Microsoft.Extensions.DependencyInjection;
using Kvesteros.Application.Database;

namespace Kvesteros.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new NpgDbConnectionFactory(connectionString));
        services.AddSingleton<DbInitializer>();
        return services;
    }
}
