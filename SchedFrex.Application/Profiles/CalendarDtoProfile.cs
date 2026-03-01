using AutoMapper;
using SchedFrex.Application.Contracts.Request;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Core.Models;

namespace SchedFrex.Application.Profiles;

public class CalendarDtoProfile : Profile
{
    public CalendarDtoProfile()
    {
        CreateMap<CalendarRequest, Calendar>()
            .ConstructUsing(src => new Calendar(src.Id, new List<Entry>()));
        CreateMap<Calendar, CalendarResponse>()
            .ForMember(cr => cr.Entries, opt => opt.MapFrom(c => c.GetEntries()));
    }
}