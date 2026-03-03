using AutoMapper;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Profiles;

public class ProblemProfile : Profile
{
    public ProblemProfile()
    {
        CreateMap<Problem, ProblemEntity>().ReverseMap();

        CreateMap<ProblemRequest, ProblemEntity>();
        CreateMap<ProblemEntity, ProblemResponse>();
    }
}