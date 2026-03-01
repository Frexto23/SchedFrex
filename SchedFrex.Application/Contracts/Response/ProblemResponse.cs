namespace SchedFrex.Application.Contracts;

public record ProblemResponse(Guid Id,
    string Title,
    int Priority,
    TimeSpan Duration,
    DateTime Deadline,
    List<TimeIntervalResponse>? TimeIntervals);