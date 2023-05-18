using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Report.Create;

public class CreateReportCommandHandler : IRequestHandler<CreateReportRequest, ErrorOr<CreateReportResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateReportCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<CreateReportResponse>> Handle(CreateReportRequest request, CancellationToken cancellationToken)
    {
        if (_dbContext.Reports.Any(r => r.Title == request.Title))
        {
            return Errors.Report.DuplicateTitle;
        }
        
        var invoices = await _dbContext.Invoices
            .Where(i => i.TransactionType == request.TransactionType & (i.CreatedDate >= request.StartDate & i.CreatedDate <= request.EndDate))
                .ToListAsync(cancellationToken);
        
        var report = new Domain.Entities.Report() {Invoices = invoices};
        _mapper.Map(request, report);
        
        await _dbContext.Reports.AddAsync(report, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
       
        return result > 0 ? new CreateReportResponse() {IsCreated = true} 
            : Errors.Database.Fail;
    }
}