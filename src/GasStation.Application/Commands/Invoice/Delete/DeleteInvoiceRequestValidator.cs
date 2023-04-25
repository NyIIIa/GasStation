using FluentValidation;

namespace GasStation.Application.Commands.Invoice.Delete;

public class DeleteInvoiceRequestValidator : AbstractValidator<DeleteInvoiceRequest>
{
    public DeleteInvoiceRequestValidator()
    {
        RuleFor(x => x.Id).Must(x => x > 0);
    }
}