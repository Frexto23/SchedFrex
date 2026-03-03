using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;

namespace SchedFrex.Application.Abstractions.Queries;

public interface IProblemsReadRepository
{
    public Task<ProblemResponse> CreateAsync(ProblemRequest problemRequest);
    public Task<ProblemResponse> UpdateAsync(ProblemRequest problemRequest);
    public Task DeleteAsync(ProblemRequest problemRequest);
    public Task<List<ProblemResponse>> GetByUserIdAsync(Guid userId);
}