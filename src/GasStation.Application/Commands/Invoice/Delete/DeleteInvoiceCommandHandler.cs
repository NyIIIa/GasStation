using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Invoice.Delete;

public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceRequest, DeleteInvoiceResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteInvoiceCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }   
        
    public async Task<DeleteInvoiceResponse> Handle(DeleteInvoiceRequest request, CancellationToken cancellationToken)
    {
        var invoice = await _dbContext.Invoices
                      .FirstOrDefaultAsync(i => i.Title == request.Title, cancellationToken)
                      ?? throw new Exception("The invoice with specified title doesn't exist!");
        
        _dbContext.Invoices.Remove(invoice);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? new DeleteInvoiceResponse() {IsDeleted = true} 
            : throw new Exception("Something went wrong!");
    }
}