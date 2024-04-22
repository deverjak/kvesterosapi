using Microsoft.Extensions.DependencyInjection;
using Kvesteros.Application.Database;
using Kvesteros.Application.Repositories;

namespace Kvesteros.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IHikeRepository, HikeRepository>();
        services.AddSingleton<IHikeImageRepository, HikeImageRepository>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new NpgDbConnectionFactory(connectionString));
        services.AddSingleton<DbInitializer>();
        return services;
    }
}
