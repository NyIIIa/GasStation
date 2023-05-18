namespace GasStation.Application.Common.Interfaces.Services;

public interface IDateTimeService
{
    /// <summary>
    /// Get current date in the unix time milliseconds since 1970-01-01T00:00:00.000Z.
    /// </summary>
    long UnixTimeNow { get; }
    
    /// <summary>
    /// Get the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).
    /// </summary>
    DateTime UtcNow { get; }

    DateTime ConvertUnixTimeToDate(long unixTimeInMilliseconds);
}