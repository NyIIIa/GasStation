using Microsoft.EntityFrameworkCore;

namespace GasStation.Infrastructure.Context;

public class GasStationDbContextFactory : DesignTimeDbContextFactoryBase<GasStationDbContext>
{
    protected override GasStationDbContext CreateNewInstance(DbContextOptions<GasStationDbContext> options)
    {
        return new GasStationDbContext(options);
    }
}