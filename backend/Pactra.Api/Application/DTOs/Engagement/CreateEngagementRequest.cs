namespace Pactra.Api.Application.DTOs;
public class CreateEngagementRequest
{
    public string Description { get; set; } = string.Empty;
    public string Goals { get; set; } = string.Empty;
    public string RequestedFeatures { get; set; } = string.Empty;
    public string Constraints { get; set; } = string.Empty;
    public decimal? Budget { get; set; }
    public DateTimeOffset? DesiredStartDate { get; set; }
    public string? AdditionalInfo { get; set; }
}