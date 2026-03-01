namespace SchedFrex.DataAccess.Entities;

public class EntryEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public TimeIntervalEntity? Slot { get; set; }
    
    public Guid CalendarId { get; set; }
    public CalendarEntity? Calendar { get; set; }
}