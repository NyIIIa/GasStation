using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Application.Common.Interfaces.Services;
using GasStation.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.Invoice.Create;

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceRequest, CreateInvoiceResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IDateTimeService _dateTimeService;

    public CreateInvoiceCommandHandler(IApplicationDbContext dbContext, IDateTimeService dateTimeService)
    {
        _dbContext = dbContext;
        _dateTimeService = dateTimeService;
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

        //further possible using AutoMapper
        var invoice = new Domain.Entities.Invoice()
        {
            Title = request.Title,
            CreatedDate = _dateTimeService.Now,
            TransactionType = request.TransactionType,
            Consumer = request.Consumer,
            Provider = request.Provider,
            TotalFuelQuantity = request.TotalFuelQuantity,
            TotalPrice = fuel.Price * (decimal) request.TotalFuelQuantity,
            Fuel = fuel
        };

        await _dbContext.Invoices.AddAsync(invoice, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
       
        return result > 0 ? new CreateInvoiceResponse() {IsCreated = true} 
            : throw new Exception("Something went wrong!");
    }
}