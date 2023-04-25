using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Application.Queries.Fuel.GetAll;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Queries.Invoice.GetAll;

public class GetAllInvoicesCommandHandler : IRequestHandler<GetAllInvoicesRequest, IReadOnlyList<GetAllInvoicesResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllInvoicesCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<GetAllInvoicesResponse>> Handle(GetAllInvoicesRequest request, CancellationToken cancellationToken)
    {
        var invoices = await _dbContext.Invoices
            .Include(i => i.Fuel)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<GetAllInvoicesResponse>>(invoices);
    }
}