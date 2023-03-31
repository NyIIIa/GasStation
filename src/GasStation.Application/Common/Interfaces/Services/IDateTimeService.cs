namespace GasStation.Application.Common.Interfaces.Services;

public interface IDateTimeService
{
    /// <summary>
    /// Get current date in the unix time milliseconds
    /// </summary>
    long Now { get; }
}