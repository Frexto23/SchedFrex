using SchedFrex.Core.Models;

namespace SchedFrex.Core.Abstractions;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetUserByNameAsync(string userName);
}