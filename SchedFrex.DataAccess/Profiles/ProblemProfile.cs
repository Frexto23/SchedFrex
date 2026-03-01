using AutoMapper;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Profiles;

public class ProblemProfile : Profile
{
    public ProblemProfile()
    {
        CreateMap<Problem, ProblemEntity>().ReverseMap();
    }
}