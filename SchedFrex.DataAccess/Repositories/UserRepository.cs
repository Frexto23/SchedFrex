using AutoMapper;
using SchedFrex.Core.Abstractions;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Repositories;

public class UserRepository : Repository<User, UserEntity>, IUserRepository
{
    public UserRepository(CalendarDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}