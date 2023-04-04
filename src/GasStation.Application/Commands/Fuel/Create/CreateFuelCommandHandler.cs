using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;

namespace GasStation.Application.Commands.Fuel.Create;

public class CreateFuelCommandHandler : IRequestHandler<CreateFuelRequest, CreateFuelResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateFuelCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<CreateFuelResponse> Handle(CreateFuelRequest request, CancellationToken cancellationToken)
    {
        var isFuelExists = _dbContext.Fuels.Any(f => f.Title == request.Title);
        if (isFuelExists)
        {
            throw new Exception("The fuel with specified title already exists!");
        }
        
        var fuel =_mapper.Map<Domain.Entities.Fuel>(request);

        await _dbContext.Fuels.AddAsync(fuel, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
       
       return result > 0 ? new CreateFuelResponse() {IsCreated = true} 
           : throw new Exception("Something went wrong!");
    }
}