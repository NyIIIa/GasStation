using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Application.Common.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Fuel.Update;

public class UpdateFuelCommandHandler : IRequestHandler<UpdateFuelRequest, UpdateFuelResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public UpdateFuelCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<UpdateFuelResponse> Handle(UpdateFuelRequest request, CancellationToken cancellationToken)
    {
        var fuel = await _dbContext.Fuels
           .FirstOrDefaultAsync(f => f.Title == request.Title, cancellationToken);
        if (fuel is null)
        {
            throw new Exception("A fuel with specified title doesn't exist!");
        }
        
        _mapper.Map(request, fuel);
        
        _dbContext.Fuels.Update(fuel);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? new UpdateFuelResponse() {IsUpdated = true} 
            : throw new Exception("Something went wrong!");
    }
}