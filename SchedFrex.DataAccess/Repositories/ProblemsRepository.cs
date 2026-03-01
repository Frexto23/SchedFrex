using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchedFrex.Core.Abstractions;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Repositories;

public class ProblemsRepository : Repository<Problem, ProblemEntity>, IProblemsRepository
{
    public Task<List<Problem>> GetByUserIdAsync(Guid userId)
    {
        return DbContext.Problems
            .Where(pe => pe.UserId == userId)
            .AsNoTracking()
            .ProjectTo<Problem>(Mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public ProblemsRepository(CalendarDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}