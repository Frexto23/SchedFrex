using Microsoft.AspNetCore.Mvc;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Request;

namespace SchedFrex.Api.Controllers;

[ApiController]
[Route("problems/")]
public class ProblemsController : ControllerBase
{
    private readonly IProblemService _problemService;

    public ProblemsController(IProblemService problemService)
    {
        _problemService = problemService;
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<List<ProblemResponse>>> GetByUserId(Guid userId)
    {
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