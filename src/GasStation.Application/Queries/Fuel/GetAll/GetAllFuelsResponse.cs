namespace GasStation.Application.Queries.Fuel.GetAll;

public class GetAllFuelsResponse
{
    public string Title { get; set; } = null!;
    public double Quantity { get; set; }
    public decimal Price { get; set; }
    public long PriceChaneDate { get; set; }
}