using AutoMapper;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Core.Abstractions;

namespace SchedFrex.Application.Services;

public class CalendarService : ICalendarService
{
    private readonly ICalendarRepository _calendarRepository;
    private readonly IMapper _mapper;

    public CalendarService(ICalendarRepository calendarRepository, IMapper mapper)
    {
        _calendarRepository = calendarRepository;
        _mapper = mapper;
    }

    public async Task<CalendarResponse> GetAsync(Guid id)
    {
        return _mapper.Map<CalendarResponse>(await _calendarRepository.GetAsync(id));
    }

    public async Task<List<CalendarResponse>> GetByUserIdAsync(Guid userId)
    {
        return _mapper.Map<List<CalendarResponse>>(await _calendarRepository.GetByUserIdAsync(userId));
    }

    public async Task<CalendarResponse> CreateAsync(Guid userId)
    {
        var response = await _calendarRepository.CreateAsync(userId);
        return _mapper.Map<CalendarResponse>(response);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _calendarRepository.DeleteAsync(id);
    }
}