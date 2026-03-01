using AutoMapper;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Authorization;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Core.Abstractions;
using SchedFrex.Core.Models;

namespace SchedFrex.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly JwtProvider _jwtProvider;

    public AuthenticationService(IUserRepository userRepository, IMapper mapper, JwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtProvider = jwtProvider;
    }

    public async Task<string?> Login(UserRequest userRequest)
    {
        var user = await _userRepository.GetUserByNameAsync(userRequest.UserName);
        if (user == null
            || !PasswordHasher.Verify(userRequest.Password, user.PasswordHash!))
        {
            return null;
        }

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }
}