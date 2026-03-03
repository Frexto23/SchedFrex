using AutoMapper;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Abstractions.Commands;
using SchedFrex.Application.Authorization;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Core.Abstractions;
using SchedFrex.Core.Models;

namespace SchedFrex.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUsersWriteRepository _usersWriteRepository;
    private readonly IMapper _mapper;
    private readonly JwtProvider _jwtProvider;

    public AuthenticationService(IUsersWriteRepository usersWriteRepository, IMapper mapper, JwtProvider jwtProvider)
    {
        _usersWriteRepository = usersWriteRepository;
        _mapper = mapper;
        _jwtProvider = jwtProvider;
    }

    public async Task<string?> Login(UserRequest userRequest)
    {
        var user = await _usersWriteRepository.GetUserByNameAsync(userRequest.UserName);
        if (user == null
            || !PasswordHasher.Verify(userRequest.Password, user.PasswordHash!))
        {
            return null;
        }

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }
}