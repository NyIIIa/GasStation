using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Report.Update;

public class UpdateReportCommandHandler : IRequestHandler<UpdateReportRequest, UpdateReportResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateReportCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
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
        _mapper.Map(request, report);
        
        _dbContext.Reports.Update(report);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? new UpdateReportResponse() {IsUpdated = true} 
            : throw new Exception("Something went wrong!");
    }
}