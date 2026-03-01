namespace SchedFrex.Application.Contracts.Request;

public record ProblemRequest(Guid Id,
    string Title,
    int Priority,
    TimeSpan Duration,
    List<TimeIntervalRequest> TimeIntervals,
    DateTime Deadline,
    Guid UserId);