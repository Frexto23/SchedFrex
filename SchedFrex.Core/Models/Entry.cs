namespace SchedFrex.Core.Models;

public class Entry
{
    public Guid Id { get; }
    public string Title { get; }
    public TimeInterval Slot { get; }
}