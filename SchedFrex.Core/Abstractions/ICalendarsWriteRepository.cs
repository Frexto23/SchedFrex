using SchedFrex.Core.Models;

namespace SchedFrex.Core.Abstractions;

public interface ICalendarsWriteRepository
{
    public Task<Calendar?> GetAsync(Guid id);
    public Task<Calendar> UpdateAsync(Calendar calendar);
}