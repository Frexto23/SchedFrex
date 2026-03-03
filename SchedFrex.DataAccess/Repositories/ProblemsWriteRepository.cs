using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Core.Abstractions;
using SchedFrex.Core.Models;

namespace SchedFrex.DataAccess.Repositories;

public class ProblemsWriteRepository : IProblemsWriteRepository
{
    private readonly CalendarDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public ProblemsWriteRepository(CalendarDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public Task<List<Problem>> GetByUserIdAsync(Guid userId)
    {
        return _dbContext.Problems
            .Where(pe => pe.UserId == userId)
            .AsNoTracking()
            .ProjectTo<Problem>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}