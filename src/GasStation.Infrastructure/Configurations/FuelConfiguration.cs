using GasStation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GasStation.Infrastructure.Configurations;

public class FuelConfiguration : IEntityTypeConfiguration<Fuel>
{
    public void Configure(EntityTypeBuilder<Fuel> builder)
    {
        //configure properties
        builder.Property(p => p.Title).HasMaxLength(10).IsRequired();
        builder.HasIndex(p => p.Title).IsUnique();
        
        //configure relationships
        builder.HasMany(f => f.Invoices).WithOne(i => i.Fuel);
    }
}