namespace GasStation.WebApi.CORS;

public class CorsSettings
{
    public const string SectionName = "CORS-Settings";
    public string[] AllowedOrigins { get; set; } = null!;
    public string[] AllowedMethods { get; set; } = null!;
}