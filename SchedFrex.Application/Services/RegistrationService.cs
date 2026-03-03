using AutoMapper;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Abstractions.Commands;
using SchedFrex.Application.Authorization;
using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Core.Abstractions;
using SchedFrex.Core.Models;

namespace SchedFrex.Application.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IUsersWriteRepository _usersWriteRepository;

    public RegistrationService(IUsersWriteRepository usersWriteRepository)
    {
        _usersWriteRepository = usersWriteRepository;
    }

    public async Task<Guid> RegisterUserAsync(UserRequest userRequest)
    {
        var passwordHash = PasswordHasher.HashPassword(userRequest.Password);
        var user = new User(userRequest.UserName, passwordHash);

        var response = await _usersWriteRepository.CreateAsync(user);

        return response.Id;
    }
}