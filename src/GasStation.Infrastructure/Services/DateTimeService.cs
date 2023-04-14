using GasStation.Application.Common.Interfaces.Services;

namespace GasStation.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public long Now => DateTimeOffset.Now.ToUnixTimeMilliseconds();
}