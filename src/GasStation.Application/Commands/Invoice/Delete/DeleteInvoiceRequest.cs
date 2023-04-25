using MediatR;
using ErrorOr;

namespace GasStation.Application.Commands.Invoice.Delete;

public class DeleteInvoiceRequest : IRequest<ErrorOr<DeleteInvoiceResponse>>
{
    public int Id { get; set; }
}