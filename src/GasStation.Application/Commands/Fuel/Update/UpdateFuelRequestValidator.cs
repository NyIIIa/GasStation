using FluentValidation;

namespace GasStation.Application.Commands.Fuel.Update;

public class UpdateFuelRequestValidator : AbstractValidator<UpdateFuelRequest>
{
    public UpdateFuelRequestValidator()
    {
        RuleFor(x => x.Title).Length(2, 10);
        RuleFor(x => x.NewPrice).Must(x => x >= 0).WithMessage("The new price of fuel should be positive!");
    }
}