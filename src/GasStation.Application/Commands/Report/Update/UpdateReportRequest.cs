using MediatR;
using ErrorOr;

namespace GasStation.Application.Commands.Report.Update;

public class UpdateReportRequest : IRequest<ErrorOr<UpdateReportResponse>>
{
    public string CurrentTitle { get; set; } = null!;
    public string NewTitle { get; set; } = null!;
}