using SchedFrex.Application.Contracts.Request;

namespace SchedFrex.Application.Abstractions.Commands;

public interface IAuthenticationService
{
    public Task<string?> Login(UserRequest userRequest);
}