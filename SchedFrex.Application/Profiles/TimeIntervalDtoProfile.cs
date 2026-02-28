using AutoMapper;
using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Core.Models;

namespace SchedFrex.Application.Profiles;

public class TimeIntervalDtoProfile : Profile
{
    public TimeIntervalDtoProfile()
    {
        CreateMap<TimeIntervalRequest, TimeInterval>()
            .ConstructUsing(src => new TimeInterval(src.Start, src.End));
        CreateMap<TimeInterval, TimeIntervalResponse>();
    }
}