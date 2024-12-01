using Drivers.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Drivers.Db;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDriversDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDriversRepository, DriversRepository>();
        return services.AddDbContext<DriversDBContext>(options => 
        {
            options.UseNpgsql(configuration.GetConnectionString(name: "DriversDb"));
        });
    }

}
