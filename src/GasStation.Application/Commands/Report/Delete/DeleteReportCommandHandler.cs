using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Report.Delete;

public class DeleteReportCommandHandler : IRequestHandler<DeleteReportRequest, DeleteReportResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteReportCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<DeleteReportResponse> Handle(DeleteReportRequest request, CancellationToken cancellationToken)
    {
        var report = await _dbContext.Reports
                         .FirstOrDefaultAsync(r => r.Title == request.Title, cancellationToken)
                         ?? throw new Exception("The report with specified title doesn't exist!");
        
        _dbContext.Reports.Remove(report);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? new DeleteReportResponse() {IsDeleted = true} 
            : throw new Exception("Something went wrong!");
    }
}