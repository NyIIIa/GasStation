using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Domain.Enums;
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
        
        //TODO # 1
        //Verify if request.TransactionType == TransactionType.Sell => request.TotalFuelQuantity < fuel.Quantity  
        
        var invoice = new Domain.Entities.Invoice() {Fuel = fuel};
        _mapper.Map(request, invoice);
        
        await _dbContext.Invoices.AddAsync(invoice, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
       
        //TODO #2
        //If result > 0 => send a notification to message bus that invoice has created
        return result > 0 ? new CreateInvoiceResponse() {IsCreated = true} 
            : Errors.Database.Fail;
    }
}