using AutoMapper;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Abstractions.Commands;
using SchedFrex.Application.Abstractions.Queries;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Application.Logic.SchedulerStrategies;
using SchedFrex.Core.Abstractions;
using SchedFrex.Core.Models;

namespace SchedFrex.Application.Services;

public class ScheduleService : IScheduleService
{
    private readonly IMapper _mapper;
    private readonly IProblemsWriteRepository _problemsRepository;
    private readonly ICalendarsWriteRepository _calendarsRepository;
    private readonly ISchedulerStrategy _scheduler;

    public ScheduleService(IMapper mapper, IProblemsReadRepository problemsReadRepository, ISchedulerStrategy scheduler, IProblemsWriteRepository problemsRepository, ICalendarsWriteRepository calendarsRepository)
    {
        _mapper = mapper;
        _scheduler = scheduler;
        _problemsRepository = problemsRepository;
        _calendarsRepository = calendarsRepository;
    }

    public async Task<CalendarResponse?> ScheduleAsync(Guid userId, ScheduleRequest scheduleRequest)
    {
        var (calendarRequest, planningRangeRequest) = scheduleRequest;
        
        var calendar = await _calendarsRepository.GetAsync(calendarRequest.Id);
        if (calendar == null) return null;
        
        var problemList = await _problemsRepository.GetByUserIdAsync(userId);
        var planningRange = _mapper.Map<TimeInterval>(planningRangeRequest);

        var result = _scheduler.Schedule(problemList, calendar, planningRange);
        if (result == null) return null;
        
        calendar.InsertEntries(result);

        await _calendarsRepository.UpdateAsync(calendar);

        return _mapper.Map<CalendarResponse>(calendar);
    }
}