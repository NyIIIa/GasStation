using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Report.Delete;

public class DeleteReportCommandHandler : IRequestHandler<DeleteReportRequest, ErrorOr<DeleteReportResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteReportCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ErrorOr<DeleteReportResponse>> Handle(DeleteReportRequest request, CancellationToken cancellationToken)
    {
        var report = await _dbContext.Reports
                         .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        if (report is null)
        {
            return Errors.Report.IdNotFound;
        }
        
        _dbContext.Reports.Remove(report);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? new DeleteReportResponse() {IsDeleted = true} 
            : Errors.Database.Fail;
    }
}