using Drivers.Db.Configuration;
using Drivers.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Drivers.Db;

public class DriversDBContext : DbContext
{
    private readonly string tenantId;

    public DriversDBContext(DbContextOptions<DriversDBContext> options) : base(options)
    {
    }

    public virtual DbSet<Driver> Drivers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DriverEntityConfiguration());
    }
}
