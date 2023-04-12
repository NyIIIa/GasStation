using ErrorOr;

namespace GasStation.Domain.Errors;

public static partial class Errors
{
    public static class Invoice
    {
        public static Error DuplicateTitle => Error.Conflict(
            code: "Invoice.DuplicateTitle",
            description: "The invoice with specified title already exists!");
        
        public static Error TitleNotFound => Error.NotFound(
            code: "Invoice.InvalidTitle",
            description: "A invoice with specified title was not found!");
    }
}