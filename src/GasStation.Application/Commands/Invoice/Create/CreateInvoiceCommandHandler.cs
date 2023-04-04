using AutoMapper;
using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Application.Common.Interfaces.Services;
using GasStation.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Invoice.Create;

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceRequest, CreateInvoiceResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateInvoiceCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CreateInvoiceResponse> Handle(CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        if (_dbContext.Invoices.Any(i => i.Title == request.Title))
        { 
            throw new Exception("The invoice with specified title already exists!");
        }
        
        var fuel = await _dbContext.Fuels
                       .FirstOrDefaultAsync(f => f.Title == request.FuelTitle, cancellationToken) 
                       ?? throw new Exception("The fuel with specified title doesn't exist!");
        
        var invoice = new Domain.Entities.Invoice() {Fuel = fuel};
        _mapper.Map(request, invoice);
        
        await _dbContext.Invoices.AddAsync(invoice, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
       
        return result > 0 ? new CreateInvoiceResponse() {IsCreated = true} 
            : throw new Exception("Something went wrong!");
    }
}