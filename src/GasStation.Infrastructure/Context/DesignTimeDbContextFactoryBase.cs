using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GasStation.Infrastructure.Context;

public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext>
    where TContext : DbContext
{
    protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);
    public TContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}GasStation.WebApi", Path.DirectorySeparatorChar);
            
        DbContextOptionsBuilder<TContext> builder = new DbContextOptionsBuilder<TContext>();
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        builder.UseSqlServer(configuration.GetConnectionString(ConnectionString.DefaultSectionName));
        return CreateNewInstance(builder.Options);
    }
}