using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace Drivers.Db;

public class DriversDBContextFactory : IDesignTimeDbContextFactory<DriversDBContext>
{
    public DriversDBContext CreateDbContext(string[] args)
    {
        var dataSource = new NpgsqlDataSourceBuilder("Host=localhost;").Build();
        var optionsBuilder = new DbContextOptionsBuilder<DriversDBContext>()
            .UseNpgsql(dataSource);

        return new DriversDBContext(optionsBuilder.Options);
    }

    public DriversDBContext CreateDbContext(string connectionString)
    {
        var dataSource = new NpgsqlDataSourceBuilder(connectionString).Build();
        var optionsBuilder = new DbContextOptionsBuilder<DriversDBContext>()
            .UseNpgsql(dataSource);

        return new DriversDBContext(optionsBuilder.Options);
    }
}
