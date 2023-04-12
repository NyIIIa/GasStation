using ErrorOr;

namespace GasStation.Domain.Errors;

public static partial class Errors
{
    public static class Database
    {
        public static Error Unexpected => Error.Unexpected(
            code: "Database.Unexpected",
            description: "The unexpected error has occurred on the database side while processing your request!");
    }
}