using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Queries.Report.GetAll;

public class GetAllReportsCommandHandler : IRequestHandler<GetAllReportsRequest,
                                           IEnumerable<GetAllReportsResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllReportsCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetAllReportsResponse>> Handle(GetAllReportsRequest request, CancellationToken cancellationToken)
    {
        var reports = await _dbContext.Reports
                            .Include(r => r.Invoices)
                            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<GetAllReportsResponse>>(reports);
    }
}