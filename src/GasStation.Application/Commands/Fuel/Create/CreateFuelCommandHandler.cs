using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;

namespace GasStation.Application.Commands.Fuel.Create;

public class CreateFuelCommandHandler : IRequestHandler<CreateFuelRequest, CreateFuelResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateFuelCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<CreateFuelResponse> Handle(CreateFuelRequest request, CancellationToken cancellationToken)
    {
        var isFuelExists = _dbContext.Fuels.Any(f => f.Title == request.Title);
        if (isFuelExists)
        {
            throw new Exception("The fuel with specified title already exists!");
        }

        //further possible using AutoMapper
        var fuel = new Domain.Entities.Fuel
        {
            Title = request.Title,
            Price = request.Price,
            Quantity = request.Quantity
        };

       await _dbContext.Fuels.AddAsync(fuel, cancellationToken);
       var result = await _dbContext.SaveChangesAsync(cancellationToken);
       
       return result > 0 ? new CreateFuelResponse() {IsCreated = true} 
           : throw new Exception("Something went wrong!");
    }
}