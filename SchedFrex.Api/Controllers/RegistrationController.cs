using Microsoft.AspNetCore.Mvc;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Request;

namespace SchedFrex.Api.Controllers;

[ApiController]
[Route("registration/")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Register(UserRequest userDto)
    {
        return await _registrationService.RegisterUserAsync(userDto);
    }
}