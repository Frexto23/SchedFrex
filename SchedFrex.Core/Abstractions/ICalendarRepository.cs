using SchedFrex.Core.Models;

namespace SchedFrex.Core.Abstractions;

public interface ICalendarRepository
{
    public Task<Calendar?> GetAsync(Guid id);
    public Task<List<Calendar>> GetByUserIdAsync(Guid userId);
    public Task<Calendar> CreateAsync(Guid userId);
    public Task DeleteAsync(Guid id);
}