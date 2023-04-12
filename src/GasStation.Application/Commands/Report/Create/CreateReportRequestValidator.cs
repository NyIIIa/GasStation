using FluentValidation;

namespace GasStation.Application.Commands.Report.Create;

public class CreateReportRequestValidator : AbstractValidator<CreateReportRequest>
{
    public CreateReportRequestValidator()
    {
        RuleFor(x => x.Title).Length(5, 60);
        RuleFor(x => x.TransactionType).NotNull();
    }
}