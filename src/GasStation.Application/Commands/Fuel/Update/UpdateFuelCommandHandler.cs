using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Application.Common.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Fuel.Update;

public class UpdateFuelCommandHandler : IRequestHandler<UpdateFuelRequest, UpdateFuelResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IDateTimeService _dateTimeService;

    public UpdateFuelCommandHandler(IApplicationDbContext dbContext, IDateTimeService dateTimeService)
    {
        _dbContext = dbContext;
        _dateTimeService = dateTimeService;
    }
    
    public async Task<UpdateFuelResponse> Handle(UpdateFuelRequest request, CancellationToken cancellationToken)
    {
        var fuel = await _dbContext.Fuels
           .FirstOrDefaultAsync(f => f.Title == request.Title, cancellationToken);
        if (fuel is null)
        {
            throw new Exception("A fuel with specified title doesn't exist!");
        }

        fuel.Price = request.NewPrice;
        fuel.PriceChangeDate = _dateTimeService.Now;

        _dbContext.Fuels.Update(fuel);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? new UpdateFuelResponse() {IsUpdated = true} 
            : throw new Exception("Something went wrong!");
    }
}