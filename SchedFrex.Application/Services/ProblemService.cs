using AutoMapper;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Core.Abstractions;
using SchedFrex.Core.Models;

namespace SchedFrex.Application.Services;

public class ProblemService : IProblemService
{
    private readonly IProblemsRepository _problemsRepository;
    private readonly IMapper _mapper;

    public ProblemService(IProblemsRepository problemsRepository, IMapper mapper)
    {
        _problemsRepository = problemsRepository;
        _mapper = mapper;
    }

    public async Task<List<ProblemResponse>> GetByUserIdAsync(Guid userId)
    {
        var response = (await _problemsRepository.GetByUserIdAsync(userId));
        return _mapper.Map<List<ProblemResponse>>(response);
    }
    
    public async Task<ProblemResponse> CreateAsync(ProblemRequest problemRequest)
    {
        var problem = _mapper.Map<Problem>(problemRequest);
        var response = await _problemsRepository.CreateAsync(problem);
        return _mapper.Map<ProblemResponse>(response);
    }

    public async Task<ProblemResponse> UpdateAsync(ProblemRequest updProblemRequest)
    {
        var updProblem = _mapper.Map<Problem>(updProblemRequest);
        var response = await _problemsRepository.UpdateAsync(updProblem);
        return _mapper.Map<ProblemResponse>(response);
    }

    public async Task DeleteAsync(ProblemRequest problemRequest)
    {
        var problem = _mapper.Map<Problem>(problemRequest);
        await _problemsRepository.DeleteAsync(problem);
    }
}