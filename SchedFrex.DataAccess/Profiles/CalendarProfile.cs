using AutoMapper;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Profiles;

public class CalendarProfile : Profile
{
    public CalendarProfile()
    {
        CreateMap<Calendar, CalendarEntity>().ReverseMap();
    }
}