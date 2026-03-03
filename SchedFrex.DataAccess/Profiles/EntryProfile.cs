using AutoMapper;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Profiles;

public class EntryProfile : Profile
{
    public EntryProfile()
    {
        CreateMap<Entry, EntryEntity>();
        CreateMap<EntryEntity, Entry>()
            .ConstructUsing(src => new Entry(src.Id, src.Title, new TimeInterval(src.Slot.Start, src.Slot.End)));

        CreateMap<EntryEntity, EntryResponse>();
    }
}