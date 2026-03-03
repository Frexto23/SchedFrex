using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedFrex.Api.Extensions;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Abstractions.Commands;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Application.Logic.SchedulerStrategies;

namespace SchedFrex.Api.Controllers;

[ApiController]
[Route("schedule/")]
[Authorize]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpPost]
    public async Task<ActionResult<CalendarResponse>> Get(ScheduleRequest scheduleRequest)
    {
        var userId = User.GetUserId();

        var response = await _scheduleService.ScheduleAsync(userId, scheduleRequest);
        if (response == null) BadRequest();
        return Ok(response);
    } 
}