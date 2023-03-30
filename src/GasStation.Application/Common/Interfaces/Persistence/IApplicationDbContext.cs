using GasStation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Common.Interfaces.Persistence;

public interface IApplicationDbContext
{
    DbSet<Fuel> Fuels { get; set; }
    DbSet<Invoice> Invoices { get; set; }
    DbSet<Report> Reports { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}