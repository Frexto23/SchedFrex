namespace SchedFrex.DataAccess.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public List<CalendarEntity>? Calendars { get; set; } = [];
    public List<ProblemEntity>? Problems { get; set; } = [];
}