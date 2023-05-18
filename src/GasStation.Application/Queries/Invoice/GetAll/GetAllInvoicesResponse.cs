using GasStation.Domain.Enums;

namespace GasStation.Application.Queries.Invoice.GetAll;

public class GetAllInvoicesResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string CreatedDate { get; set; } = null!;
    public string TransactionType { get; set; } = null!;
    public string Consumer { get; set; } = null!;
    public string Provider { get; set; } = null!;        
    public decimal TotalPrice { get; set; } 
    public double TotalFuelQuantity { get; set; }
    public Domain.Entities.Fuel Fuel { get; set; } = null!;
}