using SchedFrex.Core.Models;

namespace SchedFrex.Core.Abstractions;

public interface IUsersWriteRepository
{
    public Task<User?> GetUserByNameAsync(string userName);
    public Task<User> CreateAsync(User user);
}