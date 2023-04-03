using GasStation.Domain.Enums;
using MediatR;

namespace GasStation.Application.Commands.Report.Create;

public class CreateReportRequest : IRequest<CreateReportResponse>
{
    public string Title { get; set; } = null!;
    public long StartDate { get; set; }
    public long EndDate { get; set; }
    public TransactionType TransactionType { get; set; }
}