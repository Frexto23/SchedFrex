using AutoMapper;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Profiles;

public class TimeIntervalProfile : Profile
{
    public TimeIntervalProfile()
    {
        CreateMap<TimeInterval, TimeIntervalEntity>().ReverseMap();
    }
}