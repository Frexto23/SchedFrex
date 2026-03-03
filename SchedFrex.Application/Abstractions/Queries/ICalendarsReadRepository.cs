using SchedFrex.Application.Contracts.Response;

namespace SchedFrex.Application.Abstractions.Queries;

public interface ICalendarsReadRepository
{
    public Task<CalendarResponse?> GetAsync(Guid id);
    public Task<List<CalendarResponse>> GetByUserIdAsync(Guid userId);
    public Task<CalendarResponse> CreateAsync(Guid userId);
    public Task DeleteAsync(Guid id);
}