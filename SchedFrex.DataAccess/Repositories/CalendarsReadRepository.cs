using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;
using SchedFrex.Application.Abstractions.Queries;
using SchedFrex.Application.Contracts.Response;

namespace SchedFrex.DataAccess.Repositories;

public class CalendarsReadRepository : ICalendarsReadRepository
{
    private readonly IMapper _mapper;
    private readonly CalendarDbContext _dbContext;

    public CalendarsReadRepository(IMapper mapper, CalendarDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public Task<CalendarResponse?> GetAsync(Guid id)
    {
        return _dbContext.Calendars
            .AsNoTracking()
            .Where(c => c.Id == id)
            .ProjectTo<CalendarResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public Task<List<CalendarResponse>> GetByUserIdAsync(Guid userId)
    {
        return _dbContext.Calendars
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .ProjectTo<CalendarResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<CalendarResponse> CreateAsync(Guid userId)
    {
        var calendarEntity = new CalendarEntity
        {
            UserId = userId
        };

        calendarEntity = _dbContext.Calendars.Add(calendarEntity).Entity;
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<CalendarResponse>(calendarEntity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _dbContext.Calendars
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbContext.SaveChangesAsync();
    }
}