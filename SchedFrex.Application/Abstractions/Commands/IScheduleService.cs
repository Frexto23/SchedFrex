using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;

namespace SchedFrex.Application.Abstractions.Commands;

public interface IScheduleService
{
    public Task<CalendarResponse?> ScheduleAsync(Guid userId, ScheduleRequest scheduleRequest);
}