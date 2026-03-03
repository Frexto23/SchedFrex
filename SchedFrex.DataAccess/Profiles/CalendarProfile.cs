using AutoMapper;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Profiles;

public class CalendarProfile : Profile
{
    public CalendarProfile()
    {
        CreateMap<Calendar, CalendarEntity>().ReverseMap();

        CreateMap<CalendarRequest, CalendarEntity>();
        CreateMap<CalendarEntity, CalendarResponse>();
    }
}