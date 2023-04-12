using GasStation.Domain.Enums;
using MediatR;
using ErrorOr;

namespace GasStation.Application.Commands.Invoice.Create;

public class CreateInvoiceRequest : IRequest<ErrorOr<CreateInvoiceResponse>>
{
    public string Title { get; set; } = null!;
    public TransactionType TransactionType { get; set; }
    public string Consumer { get; set; } = null!;
    public string Provider { get; set; } = null!;
    public double TotalFuelQuantity { get; set; }
    public string FuelTitle { get; set; } = null!;
}