using MediatR;
using ErrorOr;

namespace GasStation.Application.Commands.Fuel.Update;

public class UpdateFuelRequest : IRequest<ErrorOr<UpdateFuelResponse>>
{
    public int Id { get; set; }
    public decimal NewPrice { get; set; }
}