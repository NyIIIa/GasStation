using FluentValidation;

namespace GasStation.Application.Commands.Invoice.Update;

public class UpdateInvoiceRequestValidator : AbstractValidator<UpdateInvoiceRequest>
{
    public UpdateInvoiceRequestValidator()
    {
        RuleFor(x => x.Id).Must(x => x > 0);
        RuleFor(x => x.NewTitle).NotNull();
        RuleFor(x => x.TransactionType).NotNull();
        RuleFor(x => x.Consumer).NotNull();
        RuleFor(x => x.Provider).NotNull();
        RuleFor(x => x.TotalFuelQuantity).Must(x => x > 0);
    }
}