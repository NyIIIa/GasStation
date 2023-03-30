using MediatR;

namespace GasStation.Application.Queries.User.Login;

public class LoginUserRequest : IRequest<LoginUserResponse>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}