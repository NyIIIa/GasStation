using ErrorOr;

namespace GasStation.Domain.Errors;

public static partial class Errors
{
    public static class Report
    {
        public static Error DuplicateTitle => Error.Conflict(
            code: "Report.DuplicateTitle",
            description: "The report with specified title already exists!");

        public static Error DuplicateNewTitle => Error.Conflict(
            code: "Report.DuplicateNewTitle",
            description: "The report with new title already exists!");
        
        public static Error IdNotFound => Error.NotFound(
            code: "Report.IdNotFound",
            description: "A report with specified id was not found!");
    }
}