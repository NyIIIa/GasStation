using MediatR;

namespace GasStation.Application.Queries.Report.GetAll;

public class GetAllReportsRequest : IRequest<IEnumerable<GetAllReportsResponse>>
{
    
}