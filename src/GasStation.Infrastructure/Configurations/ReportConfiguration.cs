using GasStation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GasStation.Infrastructure.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        //configure properties
        builder.Property(p => p.Title).HasMaxLength(60).IsRequired();
        builder.HasIndex(p => p.Title).IsUnique();
        builder.Property(p => p.TransactionType).IsRequired();

        //configure relationships
        builder.HasMany(r => r.Invoices).WithMany(i => i.Reports);
    }
}