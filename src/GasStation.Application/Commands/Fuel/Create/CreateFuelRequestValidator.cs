using FluentValidation;

namespace GasStation.Application.Commands.Fuel.Create;

public class CreateFuelRequestValidator : AbstractValidator<CreateFuelRequest>
{
    public CreateFuelRequestValidator()
    {
        RuleFor(x => x.Title).Length(2, 10);
        RuleFor(x => x.Quantity).Must(x => x >= 0).WithMessage("The quantity of fuel should be positive!");
        RuleFor(x => x.Price).Must(x => x >= 0).WithMessage("The price of fuel should be positive!");
    }
}