using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Request;

namespace SchedFrex.Application.Abstractions;

public interface IRegistrationService
{
    public Task<Guid> RegisterUserAsync(UserRequest userRequest);
}