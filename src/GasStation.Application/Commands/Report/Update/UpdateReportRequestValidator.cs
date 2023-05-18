using FluentValidation;

namespace GasStation.Application.Commands.Report.Update;

public class UpdateReportRequestValidator : AbstractValidator<UpdateReportRequest>
{
    public UpdateReportRequestValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.NewTitle).NotNull().Length(5, 60);
    }
}