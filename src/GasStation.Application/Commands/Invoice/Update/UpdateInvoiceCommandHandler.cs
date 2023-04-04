using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Invoice.Update;

public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceRequest, UpdateInvoiceResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateInvoiceCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<UpdateInvoiceResponse> Handle(UpdateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var invoice = await _dbContext.Invoices
            .Include(i => i.Fuel)
            .FirstOrDefaultAsync(i => i.Title == request.CurrentTitle, cancellationToken) 
                      ?? throw new Exception("The invoice with specified title doesn't exist!");

        _mapper.Map(request, invoice);
       
        _dbContext.Invoices.Update(invoice);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
       
        return result > 0 ? new UpdateInvoiceResponse() {IsUpdated = true} 
            : throw new Exception("Something went wrong!");

    }
}