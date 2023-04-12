using FluentValidation;

namespace GasStation.Application.Commands.Invoice.Create;

public class CreateInvoiceRequestValidator : AbstractValidator<CreateInvoiceRequest>
{
    public CreateInvoiceRequestValidator()
    {
        RuleFor(x => x.Title).Length(5, 30);
        RuleFor(x => x.TransactionType).NotNull();
        RuleFor(x => x.Consumer).Length(5, 30);
        RuleFor(x => x.Provider).Length(5, 30);
        RuleFor(x => x.TotalFuelQuantity).Must(x => x > 0).WithMessage("The total fuel quantity should be positive!");
        RuleFor(x => x.FuelTitle).NotNull();
    }
}