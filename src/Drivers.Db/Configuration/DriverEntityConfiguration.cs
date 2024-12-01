using Drivers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drivers.Db.Configuration;

internal class DriverEntityConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("drivers");
        builder.HasKey(e => e.Id).HasName("pk_drivers");
        builder.HasIndex(x => x.Id).HasDatabaseName("pk_drivers_unique").IsUnique();
        builder.Property(e => e.Id).HasColumnName("id").HasColumnType("uuid").IsRequired();

        builder.Property(e => e.FirstName).HasColumnName("first_name");
        builder.Property(e => e.LastName).HasColumnName("last_name");
        builder.Property(e => e.Location).HasColumnName("location");
        builder.HasIndex(e => e.Location);
    }

}
