using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;

namespace GasStation.Application.Commands.Report.Create;

public class CreateReportCommandHandler : IRequestHandler<CreateReportRequest, CreateReportResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateReportCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<CreateReportResponse> Handle(CreateReportRequest request, CancellationToken cancellationToken)
    {
        if (_dbContext.Reports.Any(r => r.Title == request.Title))
        {
            throw new Exception("The report with specified title already exists!");
        }

        var invoices = _dbContext.Invoices
                .Where(i => i.TransactionType == request.TransactionType & 
                (i.CreatedDate >= request.StartDate & i.CreatedDate <= request.EndDate));
        
        //further possible using AutoMapper
        var report = new Domain.Entities.Report()
        {
            Title = request.Title,
            Invoices = invoices,
            TotalPrice = invoices.Sum(s => s.TotalPrice),
            TotalQuantity = invoices.Sum(s => s.TotalFuelQuantity),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TransactionType = request.TransactionType
        };
        
        await _dbContext.Reports.AddAsync(report, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
       
        return result > 0 ? new CreateReportResponse() {IsCreated = true} 
            : throw new Exception("Something went wrong!");
    }
}