namespace SchedFrex.Application.Contracts.Response;

public record EntryResponse(Guid Id,
    string Title,
    TimeIntervalResponse Slot);