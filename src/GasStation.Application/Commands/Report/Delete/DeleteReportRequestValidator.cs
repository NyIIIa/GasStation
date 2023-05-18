using FluentValidation;

namespace GasStation.Application.Commands.Report.Delete;

public class DeleteReportRequestValidator : AbstractValidator<DeleteReportRequest>
{
    public DeleteReportRequestValidator()
    {
        RuleFor(x => x.Id).NotNull();
    }
}