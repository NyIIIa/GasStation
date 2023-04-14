using GasStation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GasStation.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //configure properties
        builder.Property(p => p.Login).HasMaxLength(20).IsRequired();

        //configure relationship
        builder.HasOne(u => u.Role).WithMany(r => r.User);
    }
}