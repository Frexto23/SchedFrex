namespace SchedFrex.Core.Models;

public class Entry
{
    public Guid Id { get; }
    public string Title { get; }
    public TimeInterval Slot { get; }

    public Entry(Guid id, string title, TimeInterval slot)
    {
        Id = id;
        Title = title;
        Slot = slot;
    }
    
    public Entry(Problem problem, TimeInterval slot)
    {
        Title = problem.Title;
        Slot = slot;
    }
}