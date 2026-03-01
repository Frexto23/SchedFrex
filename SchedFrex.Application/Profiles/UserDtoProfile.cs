using AutoMapper;
using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Core.Models;

namespace SchedFrex.Application.Profiles;

public class UserDtoProfile : Profile
{
    public UserDtoProfile()
    {
        CreateMap<UserRequest, User>()
            .ForMember(u => u.PasswordHash, opt => opt.Ignore());
    }
}