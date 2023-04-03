using MediatR;

namespace GasStation.Application.Commands.Report.Update;

public class UpdateReportRequest : IRequest<UpdateReportResponse>
{
    public string CurrentTitle { get; set; } = null!;
    public string NewTitle { get; set; } = null!;
}