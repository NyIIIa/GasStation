using MediatR;
using ErrorOr;

namespace GasStation.Application.Commands.Report.Delete;

public class DeleteReportRequest : IRequest<ErrorOr<DeleteReportResponse>>
{
    public int Id { get; set; }
}