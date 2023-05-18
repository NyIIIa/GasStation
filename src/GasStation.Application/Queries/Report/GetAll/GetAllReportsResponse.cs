using GasStation.Domain.Enums;

namespace GasStation.Application.Queries.Report.GetAll;

public class GetAllReportsResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public double TotalQuantity { get; set; }
    public string StartDate { get; set; } = null!;
    public string EndDate { get; set; } = null!;
    public string TransactionType { get; set; } = null!;
    public IEnumerable<Domain.Entities.Invoice> Invoices { get; set; } = null!;
}