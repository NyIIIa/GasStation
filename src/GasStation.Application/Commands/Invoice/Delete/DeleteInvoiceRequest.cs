using MediatR;

namespace GasStation.Application.Commands.Invoice.Delete;

public class DeleteInvoiceRequest : IRequest<DeleteInvoiceResponse>
{
    public string Title { get; set; } = null!;
}