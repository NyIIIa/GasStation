namespace GasStation.Application.Queries.Fuel.GetAll;

public class GetAllFuelsResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public double Quantity { get; set; }
    public decimal Price { get; set; }
    public string PriceChangeDate { get; set; } = null!;
}