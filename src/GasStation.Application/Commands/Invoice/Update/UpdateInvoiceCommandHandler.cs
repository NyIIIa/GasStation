using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Invoice.Update;

public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceRequest, ErrorOr<UpdateInvoiceResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateInvoiceCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<UpdateInvoiceResponse>> Handle(UpdateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var invoice = await _dbContext.Invoices
            .Include(i => i.Fuel)
            .FirstOrDefaultAsync(i => i.Title == request.CurrentTitle, cancellationToken);
        if (invoice is null)
        {
            return Errors.Invoice.TitleNotFound;
        }

        _mapper.Map(request, invoice);
       
        _dbContext.Invoices.Update(invoice);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
       
        return result > 0 ? new UpdateInvoiceResponse() {IsUpdated = true} 
            : Errors.Database.Fail;
    }
}