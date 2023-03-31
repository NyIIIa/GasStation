using MediatR;

namespace GasStation.Application.Commands.Fuel.Update;

public class UpdateFuelRequest : IRequest<UpdateFuelResponse>
{
    public string Title { get; set; } = null!;
    public decimal NewPrice { get; set; }
}