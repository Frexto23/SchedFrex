using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchedFrex.Core.Abstractions;
using SchedFrex.Core.Models;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Repositories;

public class UsersWriteRepository : IUsersWriteRepository
{
    private readonly IMapper _mapper;
    private readonly CalendarDbContext _dbContext;

    public UsersWriteRepository(IMapper mapper, CalendarDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserByNameAsync(string userName)
    {
        var response = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == userName);
        
        return response != null ? _mapper.Map<User>(response) : null;
    }

    public async Task<User> CreateAsync(User user)
    {
        var entity = _mapper.Map<UserEntity>(user);

        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<User>(entity);
    }
}