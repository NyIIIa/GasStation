using ErrorOr;

namespace GasStation.Domain.Errors;

public static partial class Errors
{
    public static class Database
    {
        public static Error Fail => Error.Failure(
            code: "Database.Fail",
            description: "Unfortunately, we were unable to add the data to the database due to an error!");
    }
}