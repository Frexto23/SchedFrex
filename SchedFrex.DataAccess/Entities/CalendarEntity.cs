namespace SchedFrex.DataAccess.Entities;

public class CalendarEntity
{
    public Guid Id { get; set; }
    public List<EntryEntity>? Entries { get; set; } = [];
    
    public UserEntity? User { get; set; }
}