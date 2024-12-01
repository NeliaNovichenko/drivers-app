using Microsoft.Extensions.DependencyInjection;

namespace Drivers.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IDriversService, DriversService>();
        return services;
    }
}
