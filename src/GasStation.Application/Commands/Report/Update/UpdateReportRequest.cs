using MediatR;
using ErrorOr;

namespace GasStation.Application.Commands.Report.Update;

public class UpdateReportRequest : IRequest<ErrorOr<UpdateReportResponse>>
{
    public int Id { get; set; }
    public string NewTitle { get; set; } = null!;
}