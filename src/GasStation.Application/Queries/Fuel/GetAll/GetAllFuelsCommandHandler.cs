using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Queries.Fuel.GetAll;

public class GetAllFuelsCommandHandler : IRequestHandler<GetAllFuelsRequest, IReadOnlyList<GetAllFuelsResponse>>
{   
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllFuelsCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<IReadOnlyList<GetAllFuelsResponse>> Handle(GetAllFuelsRequest request, CancellationToken cancellationToken)
    {
        var fuels = await _dbContext.Fuels.ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<GetAllFuelsResponse>>(fuels);
    }
}