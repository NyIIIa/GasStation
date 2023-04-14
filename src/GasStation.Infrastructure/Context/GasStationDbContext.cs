using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Domain.Entities;
using GasStation.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Infrastructure.Context;

public class GasStationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Fuel> Fuels { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    public GasStationDbContext(DbContextOptions options) : base(options) { }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        //Further possible to do some logic with change tracker
        
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}