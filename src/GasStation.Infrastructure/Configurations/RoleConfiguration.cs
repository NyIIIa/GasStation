using GasStation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GasStation.Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        //configure properties


        //configure relationships
        builder.HasMany(r => r.User).WithOne(u => u.Role);

        //Base roles
        builder.HasData(
            new Role() {Id = 1, Title = "Admin"},
            new Role() {Id = 1, Title = "User"});
    }
}