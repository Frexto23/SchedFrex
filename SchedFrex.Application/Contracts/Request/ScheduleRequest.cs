namespace SchedFrex.Application.Contracts.Request;

public record ScheduleRequest(CalendarRequest CalendarRequest,
    TimeIntervalRequest PlanningRangeRequest);