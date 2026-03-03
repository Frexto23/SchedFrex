using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedFrex.Api.Extensions;
using SchedFrex.Application.Abstractions.Queries;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Attributes;

namespace SchedFrex.Api.Controllers;

[ApiController]
[Route("problems/")]
[Authorize]
public class ProblemsController : ControllerBase
{
    private readonly IProblemsReadRepository _problemsReadRepository;
    private readonly IValidator<ProblemRequest> _validator;

    public ProblemsController(IProblemsReadRepository problemsReadRepository, IValidator<ProblemRequest> validator)
    {
        _problemsReadRepository = problemsReadRepository;
        _validator = validator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProblemResponse>>> GetByUserId()
    {
        var userId = User.GetUserId();
        return Ok(await _problemsReadRepository.GetByUserIdAsync(userId));
    }
    
    [HttpPost]
    public async Task<ActionResult<ProblemResponse>> Create(ProblemRequest problemRequest)
    {
        var validationResult = await _validator.ValidateAsync(problemRequest);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToJsonResponse();
            return BadRequest(new { Errors = errors });
        }
        
        return Ok(await _problemsReadRepository.CreateAsync(problemRequest));
    }
    
    [HttpPut]
    public async Task<ActionResult<ProblemResponse>> Update(ProblemRequest problemRequest)
    {
        return Ok(await _problemsReadRepository.UpdateAsync(problemRequest));
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _problemsReadRepository.DeleteAsync(new ProblemRequest(id,
            string.Empty,
            0,
            TimeSpan.Zero,
            [],
            DateTime.Now,
            Guid.Empty));
        
        return Ok();
    }
}