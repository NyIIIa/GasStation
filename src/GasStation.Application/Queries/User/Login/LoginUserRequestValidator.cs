using FluentValidation;

namespace GasStation.Application.Queries.User.Login;

public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty().NotNull().Length(5, 20);
        RuleFor(x => x.Password).NotEmpty().NotNull().Length(5, 20);
    }
}