using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchedFrex.Core.Abstractions;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Repositories;

public class CalendarRepository : ICalendarRepository
{
    private readonly IMapper _mapper;
    private readonly CalendarDbContext _dbContext;

    public CalendarRepository(IMapper mapper, CalendarDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public Task<Calendar?> GetAsync(Guid id)
    {
        return _dbContext.Calendars
            .AsNoTracking()
            .ProjectTo<Calendar>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<List<Calendar>> GetByUserIdAsync(Guid userId)
    {
        return _dbContext.Calendars
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .ProjectTo<Calendar>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<Calendar> CreateAsync(Guid userId)
    {
        var calendarEntity = new CalendarEntity
        {
            UserId = userId
        };

        calendarEntity = _dbContext.Calendars.Add(calendarEntity).Entity;
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<Calendar>(calendarEntity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _dbContext.Calendars
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbContext.SaveChangesAsync();
    }
}