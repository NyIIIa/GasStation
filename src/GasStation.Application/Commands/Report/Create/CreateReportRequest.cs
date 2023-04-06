using GasStation.Domain.Enums;
using MediatR;
using ErrorOr;

namespace GasStation.Application.Commands.Report.Create;

public class CreateReportRequest : IRequest<ErrorOr<CreateReportResponse>>
{
    public string Title { get; set; } = null!;
    public long StartDate { get; set; }
    public long EndDate { get; set; }
    public TransactionType TransactionType { get; set; }
}