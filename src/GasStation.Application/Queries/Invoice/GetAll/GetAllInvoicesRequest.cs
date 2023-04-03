using MediatR;

namespace GasStation.Application.Queries.Invoice.GetAll;

public class GetAllInvoicesRequest : IRequest<IEnumerable<GetAllInvoicesResponse>>
{
    
}