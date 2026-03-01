using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Request;

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
        var allClaims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
        var userId = Guid.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sid));
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