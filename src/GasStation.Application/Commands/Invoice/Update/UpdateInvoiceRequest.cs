using GasStation.Domain.Enums;
using MediatR;

namespace GasStation.Application.Commands.Invoice.Update;

public class UpdateInvoiceRequest : IRequest<UpdateInvoiceResponse>
{
    public string CurrentTitle { get; set; } = null!;
    public string NewTitle { get; set; } = null!;
    public TransactionType TransactionType { get; set; }
    public string Consumer { get; set; } = null!;
    public string Provider { get; set; } = null!;
    public double TotalFuelQuantity { get; set; }
}