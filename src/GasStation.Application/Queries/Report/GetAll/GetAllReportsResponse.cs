using GasStation.Domain.Enums;

namespace GasStation.Application.Queries.Report.GetAll;

public class GetAllReportsResponse
{
    public string Title { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public double TotalQuantity { get; set; }
    public long StartDate { get; set; }
    public long EndDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public IEnumerable<Domain.Entities.Invoice> Invoices { get; set; } = null!;
}