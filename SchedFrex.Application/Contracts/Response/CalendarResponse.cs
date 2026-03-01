namespace SchedFrex.Application.Contracts.Response;

public record CalendarResponse(Guid Id, 
    List<EntryResponse> Entries);