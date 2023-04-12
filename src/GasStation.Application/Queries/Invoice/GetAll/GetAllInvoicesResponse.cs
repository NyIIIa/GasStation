using GasStation.Domain.Enums;

namespace GasStation.Application.Queries.Invoice.GetAll;

public class GetAllInvoicesResponse
{
    public string Title { get; set; } = null!;
    public long CreatedDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public string Consumer { get; set; } = null!;
    public string Provider { get; set; } = null!;        
    public decimal TotalPrice { get; set; } 
    public double TotalFuelQuantity { get; set; }
    public string FuelTitle { get; set; } = null!;
}