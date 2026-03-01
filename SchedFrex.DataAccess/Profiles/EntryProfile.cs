using AutoMapper;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Profiles;

public class EntryProfile : Profile
{
    public EntryProfile()
    {
        CreateMap<Entry, EntryEntity>().ReverseMap();
    }
}