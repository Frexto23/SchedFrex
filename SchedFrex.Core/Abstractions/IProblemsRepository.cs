using SchedFrex.Core.Models;

namespace SchedFrex.Core.Abstractions;

public interface IProblemsRepository
{
    public Task<List<Problem>> GetByUserIdAsync(Guid userId);
    public Task<Problem> CreateAsync(Problem problem);
    public Task<Problem> UpdateAsync(Problem problem);
    public Task DeleteAsync(Problem problem);
    
}