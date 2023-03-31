using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;

namespace GasStation.Application.Queries.Fuel.GetAll;

public class GetAllFuelsCommandHandler : IRequestHandler<GetAllFuelsRequest, IEnumerable<GetAllFuelsResponse>>
{   
    private readonly IApplicationDbContext _dbContext;

    public GetAllFuelsCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<IEnumerable<GetAllFuelsResponse>> Handle(GetAllFuelsRequest request, CancellationToken cancellationToken)
    {
        //further possible using AutoMapper
        var fuels = _dbContext.Fuels
            .ToList()
            .Select(f => new GetAllFuelsResponse()
            {
                Title = f.Title,
                Price = f.Price,
                PriceChaneDate = f.PriceChangeDate,
                Quantity = f.Quantity
            });
        
        return Task.FromResult(fuels);
    }
}