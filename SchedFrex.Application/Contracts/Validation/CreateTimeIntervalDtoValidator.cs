using FluentValidation;
using SchedFrex.Application.Contracts.Request;

namespace SchedFrex.Application.Contracts.Validation;

public class CreateTimeIntervalDtoValidator : AbstractValidator<TimeIntervalRequest>
{
    public CreateTimeIntervalDtoValidator()
    {
        RuleFor(x => x.Start)
            .GreaterThan(DateTime.Now)
            .WithMessage("Start must be in the future.");
        
        RuleFor(x => x.End)
            .GreaterThanOrEqualTo(x => x.Start)
            .WithMessage("End must be after Start.");
    }
}