using MediatR;

namespace GasStation.Application.Commands.User.Register;

public class RegisterUserRequest : IRequest<RegisterUserResponse>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string RoleTitle { get; set; } = null!;
}