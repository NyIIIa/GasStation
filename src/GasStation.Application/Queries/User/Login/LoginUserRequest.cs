using MediatR;
using ErrorOr;

namespace GasStation.Application.Queries.User.Login;

public class LoginUserRequest : IRequest<ErrorOr<LoginUserResponse>>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}