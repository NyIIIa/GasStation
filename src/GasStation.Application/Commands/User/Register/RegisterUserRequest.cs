using System.Text.Json.Serialization;
using MediatR;
using ErrorOr;

namespace GasStation.Application.Commands.User.Register;

public class RegisterUserRequest : IRequest<ErrorOr<RegisterUserResponse>>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    [JsonPropertyName("Role")]
    public string RoleTitle { get; set; } = null!;
}