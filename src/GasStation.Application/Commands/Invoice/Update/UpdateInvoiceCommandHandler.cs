using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Invoice.Update;

public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceRequest, UpdateInvoiceResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateInvoiceCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<UpdateInvoiceResponse> Handle(UpdateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var invoice = await _dbContext.Invoices
            .Include(i => i.Fuel)
            .FirstOrDefaultAsync(i => i.Title == request.CurrentTitle, cancellationToken) 
                      ?? throw new Exception("The invoice with specified title doesn't exist!");
        
        invoice.Title = request.NewTitle;
        invoice.TransactionType = request.TransactionType;
        invoice.Consumer = request.Consumer;
        invoice.Provider = request.Provider;
        invoice.TotalFuelQuantity = request.TotalFuelQuantity;
        invoice.TotalPrice = invoice.Fuel.Price * (decimal)request.TotalFuelQuantity;
        
        _dbContext.Invoices.Update(invoice);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
       
        return result > 0 ? new UpdateInvoiceResponse() {IsUpdated = true} 
            : throw new Exception("Something went wrong!");

    }
}