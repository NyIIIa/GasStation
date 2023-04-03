using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Queries.Report.GetAll;

public class GetAllReportsCommandHandler : IRequestHandler<GetAllReportsRequest,
                                           IEnumerable<GetAllReportsResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAllReportsCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<GetAllReportsResponse>> Handle(GetAllReportsRequest request, CancellationToken cancellationToken)
    {
        var reports = await _dbContext.Reports
                            .Include(r => r.Invoices)
                            .ToListAsync(cancellationToken);
        
        //further possible using AutoMapper
        return reports.Select(r => new GetAllReportsResponse()
        {
            Title = r.Title,
            TotalPrice = r.TotalPrice,
            TotalQuantity = r.TotalQuantity,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            TransactionType = r.TransactionType,
            Invoices = r.Invoices
        });
    }
}