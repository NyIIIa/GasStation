using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Invoice.Delete;

public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceRequest, ErrorOr<DeleteInvoiceResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteInvoiceCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }   
        
    public async Task<ErrorOr<DeleteInvoiceResponse>> Handle(DeleteInvoiceRequest request, CancellationToken cancellationToken)
    {
        var invoice = await _dbContext.Invoices
            .FirstOrDefaultAsync(i => i.Title == request.Title, cancellationToken);
        if (invoice is null)
        {
            return Errors.Invoice.TitleNotFound;
        }

        _dbContext.Invoices.Remove(invoice);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? new DeleteInvoiceResponse() {IsDeleted = true} 
            : Errors.Database.Fail;
    }
}