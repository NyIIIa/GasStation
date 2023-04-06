using ErrorOr;

namespace GasStation.Domain.Errors;

public static partial class Errors
{
    public static class Role
    {
        public static Error TitleNotFound => Error.NotFound(
            code: "Role.NotFound",
            description: "The specified role was not found!");
    } 
}