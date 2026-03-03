using FluentValidation;
using SchedFrex.Application.Contracts.Request;

namespace SchedFrex.Application.Contracts.Validation;

public class CreateScheduleDtoValidator : AbstractValidator<ScheduleRequest>
{
    public CreateScheduleDtoValidator(IValidator<TimeIntervalRequest> interValidator)
    {
        RuleFor(s => s.PlanningRangeRequest)
            .SetValidator(interValidator);
    }
}