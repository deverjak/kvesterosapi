using KvesterosApi.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        var repositoryTypes = assembly.GetTypes()
            .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IRepository<>)))
            .ToList();

        foreach (var type in repositoryTypes)
        {
            var interfaceType = type.GetInterfaces().First(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IRepository<>));
            services.AddScoped(interfaceType, type);
        }

        return services;
    }
}
