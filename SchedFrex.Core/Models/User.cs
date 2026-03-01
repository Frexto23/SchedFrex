namespace SchedFrex.Core.Models;

public class User
{
    public Guid Id { get; }
    public List<Calendar>? Calendars { get; }
    public List<Problem>? Problems { get; }
    public string? UserName { get; }
    public string? PasswordHash { get; }
    
    public User(Guid id)
    {
        Id = id;
    }

    public User(string userName, string passwordHash)
    {
        Id = Guid.Empty;
        UserName = userName;
        PasswordHash = passwordHash;
    }
    
    public User(Guid id, List<Calendar> calendars, List<Problem> problems, string passwordHash)
    {
        Id = id;
        Calendars = calendars;
        Problems = problems;
        PasswordHash = passwordHash;
    }
}