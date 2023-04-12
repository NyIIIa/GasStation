namespace GasStation.Domain.Entities;

public class Fuel
{       
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public double Quantity { get; set; }
    public decimal Price { get; set; }
    public long PriceChangeDate { get; set; }
    public IEnumerable<Invoice> Invoices { get; set; } = null!;
}