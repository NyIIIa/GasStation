using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Report.Update;

public class UpdateReportCommandHandler : IRequestHandler<UpdateReportRequest, ErrorOr<UpdateReportResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateReportCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<UpdateReportResponse>> Handle(UpdateReportRequest request, CancellationToken cancellationToken)
    {
        if (_dbContext.Reports.Any(r => r.Title == request.NewTitle))
        {
            return Errors.Report.DuplicateNewTitle;
        }
        
        var report = await _dbContext.Reports
                         .FirstOrDefaultAsync(r => r.Title == request.CurrentTitle, cancellationToken);
        if (report is null)
        {
            return Errors.Report.TitleNotFound;
        }
        
        _mapper.Map(request, report);
        
        _dbContext.Reports.Update(report);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? new UpdateReportResponse() {IsUpdated = true} 
            : Errors.Database.Fail;
    }
}