using SchedFrex.Core.Models;

namespace SchedFrex.Core.Abstractions;

public interface IProblemsWriteRepository
{
    public Task<List<Problem>> GetByUserIdAsync(Guid userId);
}