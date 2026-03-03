using FluentValidation;
using SchedFrex.Application.Contracts.Request;

namespace SchedFrex.Application.Contracts.Validation;

public class CreateProblemDtoValidator : AbstractValidator<ProblemRequest>
{
    public CreateProblemDtoValidator(IValidator<TimeIntervalRequest> interValidator)
    {
        RuleForEach(p => p.TimeIntervals)
            .NotNull()
            .SetValidator(interValidator);

        RuleFor(p => p.Deadline)
            .GreaterThanOrEqualTo(DateTime.Now);

        RuleFor(p => p.Title)
            .Length(1, 250);
    }
}