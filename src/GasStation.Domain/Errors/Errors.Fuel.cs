using ErrorOr;

namespace GasStation.Domain.Errors;

public static partial class Errors
{
    public static class Fuel
    {
        public static Error DuplicateTitle => Error.Conflict(
            code: "Fuel.DuplicateTitle",
            description: "The fuel with specified title already exists!");
        
        public static Error TitleNotFound => Error.NotFound(
            code: "Fuel.InvalidTitle",
            description: "A fuel with specified title was not found!");
    }
}