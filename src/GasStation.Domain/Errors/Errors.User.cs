using ErrorOr;

namespace GasStation.Domain.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateLogin => Error.Conflict(
            code: "User.DuplicateLogin",
            description: "The user with specified login already exists!");
        
        public static Error InvalidCredentials => Error.Failure(
            code: "User.InvalidLogin",
            description: "Invalid credentials!");
    }
}