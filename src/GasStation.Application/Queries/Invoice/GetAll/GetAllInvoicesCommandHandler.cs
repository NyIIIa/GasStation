using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Queries.Invoice.GetAll;

public class GetAllInvoicesCommandHandler : IRequestHandler<GetAllInvoicesRequest, IEnumerable<GetAllInvoicesResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAllInvoicesCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetAllInvoicesResponse>> Handle(GetAllInvoicesRequest request, CancellationToken cancellationToken)
    {
        var invoices = await _dbContext.Invoices
            .Include(i => i.Fuel)
            .ToListAsync(cancellationToken);

        //further possible using AutoMapper
        return invoices.Select(i => new GetAllInvoicesResponse()
        {
            Title = i.Title,
            CreatedDate = i.CreatedDate,
            TransactionType = i.TransactionType,
            Consumer = i.Consumer,
            Provider = i.Provider,
            TotalPrice = i.TotalPrice,
            TotalFuelQuantity = i.TotalFuelQuantity,
            FuelTitle = i.Fuel.Title
        });
    }
}