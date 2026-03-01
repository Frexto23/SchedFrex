using AutoMapper;
using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Core.Models;

namespace SchedFrex.Application.Profiles;

public class ProblemDtoProfile : Profile
{
    public ProblemDtoProfile()
    {
        CreateMap<ProblemRequest, Problem>();
        CreateMap<Problem, ProblemResponse>()
            .ForMember(pr => pr.TimeIntervals, opt => opt.MapFrom(p => p.GetTimeIntervals()));
    }
}