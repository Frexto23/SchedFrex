using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchedFrex.Core.Abstractions;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Repositories;

public class UserRepository : Repository<User, UserEntity>, IUserRepository
{
    public UserRepository(CalendarDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<User?> GetUserByNameAsync(string userName)
    {
        var response = await DbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == userName);
        
        return response != null ? Mapper.Map<User>(response) : null;
    }
}