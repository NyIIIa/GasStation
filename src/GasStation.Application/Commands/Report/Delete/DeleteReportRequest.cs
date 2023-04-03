using MediatR;

namespace GasStation.Application.Commands.Report.Delete;

public class DeleteReportRequest : IRequest<DeleteReportResponse>
{
    public string Title { get; set; } = null!;
}