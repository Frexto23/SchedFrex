using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Core.Models;

namespace SchedFrex.Application.Abstractions;

public interface IProblemService
{
    public Task<ProblemResponse> CreateAsync(ProblemRequest problemRequest);
    public Task<ProblemResponse> UpdateAsync(ProblemRequest problemRequest);
    public Task DeleteAsync(ProblemRequest problemRequest);
    public Task<List<ProblemResponse>> GetByUserIdAsync(Guid userId);
}