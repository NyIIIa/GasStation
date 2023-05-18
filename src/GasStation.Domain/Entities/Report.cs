using GasStation.Domain.Enums;

namespace GasStation.Domain.Entities;

public class Report
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public double TotalQuantity { get; set; }
    public long StartDate { get; set; }
    public long EndDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public IEnumerable<Invoice> Invoices { get; set; } = null!;
}