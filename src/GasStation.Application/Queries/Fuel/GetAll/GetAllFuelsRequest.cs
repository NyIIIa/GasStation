using MediatR;

namespace GasStation.Application.Queries.Fuel.GetAll;

public class GetAllFuelsRequest : IRequest<IEnumerable<GetAllFuelsResponse>>
{
    
}   