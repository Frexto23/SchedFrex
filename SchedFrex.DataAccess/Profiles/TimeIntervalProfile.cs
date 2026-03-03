using AutoMapper;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Profiles;

public class TimeIntervalProfile : Profile
{
    public TimeIntervalProfile()
    {
        CreateMap<TimeInterval, TimeIntervalEntity>().ReverseMap();
        
        CreateMap<TimeIntervalEntity, TimeIntervalResponse>();
        CreateMap<TimeIntervalRequest, TimeIntervalEntity>();
    }
}