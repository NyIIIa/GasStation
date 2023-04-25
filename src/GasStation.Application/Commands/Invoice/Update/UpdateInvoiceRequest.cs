using GasStation.Domain.Enums;
using MediatR;
using ErrorOr;

namespace GasStation.Application.Commands.Invoice.Update;

public class UpdateInvoiceRequest : IRequest<ErrorOr<UpdateInvoiceResponse>>
{
    public int Id { get; set; }
    public string NewTitle { get; set; } = null!;
    public TransactionType TransactionType { get; set; }
    public string Consumer { get; set; } = null!;
    public string Provider { get; set; } = null!;
    public double TotalFuelQuantity { get; set; }
}