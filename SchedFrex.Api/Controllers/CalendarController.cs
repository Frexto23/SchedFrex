using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedFrex.Api.Extensions;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Abstractions.Queries;
using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Response;

namespace SchedFrex.Api.Controllers;

[ApiController]
[Route("calendars/")]
[Authorize]
public class CalendarController : ControllerBase
{
    private readonly ICalendarsReadRepository _calendarsReadRepository;

    public CalendarController(ICalendarsReadRepository calendarsReadRepository)
    {
        _calendarsReadRepository = calendarsReadRepository;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CalendarResponse>> Get(Guid id)
    {
        return Ok(await _calendarsReadRepository.GetAsync(id));
    }

    [HttpGet]
    public async Task<ActionResult<List<CalendarResponse>>> Get()
    {
        var userId = User.GetUserId();
        return Ok(await _calendarsReadRepository.GetByUserIdAsync(userId));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _calendarsReadRepository.DeleteAsync(id);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<CalendarResponse>> Create()
    {
        var userId = User.GetUserId();
        await _calendarsReadRepository.CreateAsync(userId);

        return Ok();
    }
}