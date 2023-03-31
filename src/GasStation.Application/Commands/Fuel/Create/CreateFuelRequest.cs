using MediatR;

namespace GasStation.Application.Commands.Fuel.Create;

public class CreateFuelRequest : IRequest<CreateFuelResponse>
{
    public string Title { get; set; } = null!;
    public double Quantity { get; set; }
    public decimal Price { get; set; }
}   