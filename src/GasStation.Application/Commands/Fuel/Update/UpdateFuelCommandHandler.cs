using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Domain.Errors;
using Microsoft.EntityFrameworkCore;


namespace GasStation.Application.Commands.Fuel.Update;

public class UpdateFuelCommandHandler : IRequestHandler<UpdateFuelRequest, ErrorOr<UpdateFuelResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public UpdateFuelCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<UpdateFuelResponse>> Handle(UpdateFuelRequest request, CancellationToken cancellationToken)
    {
        var fuel = await _dbContext.Fuels
           .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        if (fuel is null)
        {
            return Errors.Fuel.TitleNotFound;
        }
        
        _mapper.Map(request, fuel);
        
        _dbContext.Fuels.Update(fuel);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? new UpdateFuelResponse() {IsUpdated = true} 
            : Errors.Database.Fail;
    }
}