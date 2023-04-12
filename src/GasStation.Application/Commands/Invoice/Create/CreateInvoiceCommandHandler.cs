using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Invoice.Create;

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceRequest, ErrorOr<CreateInvoiceResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateInvoiceCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ErrorOr<CreateInvoiceResponse>> Handle(CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        if (_dbContext.Invoices.Any(i => i.Title == request.Title))
        {
            return Errors.Invoice.DuplicateTitle;
        }

        var fuel = await _dbContext.Fuels
            .FirstOrDefaultAsync(f => f.Title == request.FuelTitle, cancellationToken);
        if (fuel is null)
        {
            return Errors.Fuel.TitleNotFound;
        }
        
        var invoice = new Domain.Entities.Invoice() {Fuel = fuel};
        _mapper.Map(request, invoice);
        
        await _dbContext.Invoices.AddAsync(invoice, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
       
        return result > 0 ? new CreateInvoiceResponse() {IsCreated = true} 
            : Errors.Database.Unexpected;
    }
}