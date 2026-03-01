using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedFrex.Api.Extensions;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Response;

namespace SchedFrex.Api.Controllers;

[ApiController]
[Route("calendars/")]
[Authorize]
public class CalendarController : ControllerBase
{
    private readonly ICalendarService _calendarService;

    public CalendarController(ICalendarService calendarService)
    {
        _calendarService = calendarService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CalendarResponse>> Get(Guid id)
    {
        return Ok(await _calendarService.GetAsync(id));
    }

    [HttpGet]
    public async Task<ActionResult<List<CalendarResponse>>> Get()
    {
        var userId = User.GetUserId();
        return Ok(await _calendarService.GetByUserIdAsync(userId));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _calendarService.DeleteAsync(id);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<CalendarResponse>> Create()
    {
        var userId = User.GetUserId();
        await _calendarService.CreateAsync(userId);

        return Ok();
    }
}