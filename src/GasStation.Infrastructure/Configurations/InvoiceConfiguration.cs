using GasStation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GasStation.Infrastructure.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        //configure properties
        builder.Property(p => p.Title).HasMaxLength(30).IsRequired();
        builder.Property(p => p.TransactionType).IsRequired();
        builder.Property(p => p.Consumer).HasMaxLength(30).IsRequired();
        builder.Property(p => p.Provider).HasMaxLength(30).IsRequired();
        
        //configure relationships
        builder.HasOne(i => i.Fuel).WithMany(f => f.Invoices);
        builder.HasMany(i => i.Reports).WithMany(r => r.Invoices);
    }
}