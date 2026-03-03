using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Core.Abstractions;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Repositories;

public class CalendarsWriteRepository : ICalendarsWriteRepository
{
    private readonly IMapper _mapper;
    private readonly CalendarDbContext _dbContext;

    public CalendarsWriteRepository(IMapper mapper, CalendarDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public Task<Calendar?> GetAsync(Guid id)
    {
        return _dbContext.Calendars
            .AsNoTracking()
            .Where(c => c.Id == id)
            .ProjectTo<Calendar>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<Calendar> UpdateAsync(Calendar calendar)
    {
        var entity = await _dbContext.Calendars
            .Include(c => c.Entries)
            .FirstOrDefaultAsync(c => c.Id == calendar.Id);

        entity.Entries = _mapper.Map<List<EntryEntity>>(calendar.GetEntries());

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<Calendar>(entity);
    }
}