using GasStation.Domain.Enums;
    
namespace GasStation.Domain.Entities;

public class Invoice
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public long CreatedDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public string Consumer { get; set; } = null!;
    public string Provider { get; set; } = null!;        
    public decimal TotalPrice { get; set; } 
    public double TotalFuelQuantity { get; set; }
    public Fuel Fuel { get; set; } = null!;
    public IEnumerable<Report> Reports { get; set; } = null!;
}      