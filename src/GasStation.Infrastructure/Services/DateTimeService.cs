using GasStation.Application.Common.Interfaces.Services;

namespace GasStation.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    private readonly DateTime _unixEpoch =
        new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public long UnixTimeNow => DateTimeOffset.Now.ToUnixTimeMilliseconds();
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime ConvertUnixTimeToDate(long unixTimeInMilliseconds)
    {
        return _unixEpoch.AddMilliseconds(unixTimeInMilliseconds);
    }
}