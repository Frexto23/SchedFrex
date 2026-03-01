using AutoMapper;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Authorization;
using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Core.Abstractions;
using SchedFrex.Core.Models;

namespace SchedFrex.Application.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IUserRepository _userRepository;

    public RegistrationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> RegisterUserAsync(UserRequest userRequest)
    {
        var passwordHash = PasswordHasher.HashPassword(userRequest.Password);
        var user = new User(userRequest.UserName, passwordHash);

        var response = await _userRepository.CreateAsync(user);

        return response.Id;
    }
}