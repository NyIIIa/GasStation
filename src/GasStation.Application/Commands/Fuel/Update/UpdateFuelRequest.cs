using MediatR;
using ErrorOr;

namespace GasStation.Application.Commands.Fuel.Update;

public class UpdateFuelRequest : IRequest<ErrorOr<UpdateFuelResponse>>
{
    public string Title { get; set; } = null!;
    public decimal NewPrice { get; set; }
}