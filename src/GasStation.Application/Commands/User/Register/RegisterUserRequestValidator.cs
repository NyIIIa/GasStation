using FluentValidation;

namespace GasStation.Application.Commands.User.Register;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty().NotNull().Length(5, 20);
        RuleFor(x => x.Password).NotEmpty().NotNull().Length(5, 20);
        RuleFor(x => x.RoleTitle).NotEmpty().NotNull();
    }
}