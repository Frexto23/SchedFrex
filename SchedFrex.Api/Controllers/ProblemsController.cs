using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedFrex.Api.Extensions;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;

namespace SchedFrex.Api.Controllers;

[ApiController]
[Route("problems/")]
[Authorize]
public class ProblemsController : ControllerBase
{
    private readonly IProblemService _problemService;

    public ProblemsController(IProblemService problemService)
    {
        _problemService = problemService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProblemResponse>>> GetByUserId()
    {
        var userId = User.GetUserId();
        return Ok(await _problemService.GetByUserIdAsync(userId));
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(ProblemRequest problemRequest)
    {
        return Ok(await _problemService.CreateAsync(problemRequest));
    }
    
    [HttpPut]
    public async Task<ActionResult<Guid>> GetByUserId(ProblemRequest problemRequest)
    {
        return Ok(await _problemService.UpdateAsync(problemRequest));
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _problemService.DeleteAsync(new ProblemRequest(id,
            string.Empty,
            0,
            TimeSpan.Zero,
            [],
            DateTime.Now,
            Guid.Empty));
        
        return Ok();
    }
}