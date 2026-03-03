using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchedFrex.Core.Models;
using SchedFrex.Application.Abstractions.Queries;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Repositories;

public class ProblemsReadRepository : IProblemsReadRepository
{
    private readonly CalendarDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public ProblemsReadRepository(CalendarDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public Task<List<ProblemResponse>> GetByUserIdAsync(Guid userId)
    {
        return _dbContext.Problems
            .Where(pe => pe.UserId == userId)
            .AsNoTracking()
            .ProjectTo<ProblemResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<ProblemResponse> CreateAsync(ProblemRequest problem)
    {
        var entity = _mapper.Map<ProblemEntity>(problem);

        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProblemResponse>(entity);
    }

    public async Task<ProblemResponse> UpdateAsync(ProblemRequest problem)
    {
        var entity = await _dbContext.Problems
            .Include(p => p.TimeIntervals)
            .FirstOrDefaultAsync(p => p.Id == problem.Id);

        if (entity == null) throw new Exception("There is no such problem in db!");

        entity.Deadline = problem.Deadline;
        entity.TimeIntervals = _mapper.Map<List<TimeIntervalEntity>>(problem.TimeIntervals);
        entity.Title = problem.Title;
        entity.Duration = problem.Duration;
        entity.Priority = problem.Priority;
        
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProblemResponse>(entity);
    }

    public async Task DeleteAsync(ProblemRequest problem)
    {
        await _dbContext.Problems
            .Where(p => p.Id == problem.Id)
            .ExecuteDeleteAsync();
        
        await _dbContext.SaveChangesAsync();
    }
}