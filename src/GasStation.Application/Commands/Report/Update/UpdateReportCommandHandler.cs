using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Report.Update;

public class UpdateReportCommandHandler : IRequestHandler<UpdateReportRequest, UpdateReportResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateReportCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<UpdateReportResponse> Handle(UpdateReportRequest request, CancellationToken cancellationToken)
    {
        if (_dbContext.Reports.Any(r => r.Title == request.NewTitle))
        {
            throw new Exception("The report with a new title already exist!");
        }
        
        var report = await _dbContext.Reports
                         .FirstOrDefaultAsync(r => r.Title == request.CurrentTitle, cancellationToken)
                         ?? throw new Exception("The report with specified title doesn't exist!");
        report.Title = request.NewTitle;

        _dbContext.Reports.Update(report);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? new UpdateReportResponse() {IsUpdated = true} 
            : throw new Exception("Something went wrong!");
    }
}