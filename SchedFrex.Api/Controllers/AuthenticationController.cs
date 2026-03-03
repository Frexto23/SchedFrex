using Microsoft.AspNetCore.Mvc;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Abstractions.Commands;
using SchedFrex.Application.Contracts.Request;

namespace SchedFrex.Api.Controllers;

[ApiController]
[Route("login/")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;

    public AuthenticationController(IAuthenticationService authenticationService, IWebHostEnvironment env, IConfiguration configuration)
    {
        _authenticationService = authenticationService;
        _env = env;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login(UserRequest userRequest)
    {
        var token = await _authenticationService.Login(userRequest);
        if (token == null) Unauthorized();
        
        Response.Cookies.Append("access-token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = !_env.IsDevelopment(), // зависит от development mode
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.Now.AddHours(_configuration.GetValue<int>("JwtToken:ExpiresHours"))
        });

        return Ok("Logged in");
    }
}