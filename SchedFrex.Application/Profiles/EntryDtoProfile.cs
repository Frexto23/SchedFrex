using AutoMapper;
using SchedFrex.Application.Contracts;
using SchedFrex.Application.Contracts.Response;
using SchedFrex.Core.Models;

namespace SchedFrex.Application.Profiles;

public class EntryDtoProfile : Profile
{
    public EntryDtoProfile()
    {
        CreateMap<Entry, EntryResponse>();
    }
}