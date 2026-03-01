namespace SchedFrex.DataAccess.Entities;

public class CalendarEntity
{
    public Guid Id { get; set; } = Guid.Empty;
    public List<EntryEntity>? Entries { get; set; } = [];
    
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
}