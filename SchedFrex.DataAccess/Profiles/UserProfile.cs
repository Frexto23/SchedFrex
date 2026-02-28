using AutoMapper;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserEntity>().ReverseMap();
    }
}