using MediatR;

namespace GasStation.Application.Queries.Fuel.GetAll;

public class GetAllFuelsRequest : IRequest<IReadOnlyList<GetAllFuelsResponse>>
{
    
}   