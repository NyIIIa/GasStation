using GasStation.Application.Common.Interfaces.Services;

namespace GasStation.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public long UnixTimeNow => DateTimeOffset.Now.ToUnixTimeMilliseconds();
    public DateTime UtcNow => DateTime.UtcNow;
}