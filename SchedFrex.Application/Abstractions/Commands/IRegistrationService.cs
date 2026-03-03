using SchedFrex.Application.Contracts.Request;

namespace SchedFrex.Application.Abstractions.Commands;

public interface IRegistrationService
{
    public Task<Guid> RegisterUserAsync(UserRequest userRequest);
}