namespace SchedFrex.Core.Models;

public class Problem
{
    public Guid Id { get; }
    public string Title { get; } 
    public int Priority { get; }
    public TimeSpan Duration { get; }
    public DateTime Deadline { get; }
    public Guid UserId { get; }
    public User? User { get; }
    
    private readonly List<TimeInterval>? TimeIntervals;

    public Problem(Guid id, string title, int priority, TimeSpan duration, DateTime deadline, Guid userId, List<TimeInterval> timeIntervals)
    {
        Id = id;
        Title = title;
        Priority = priority;
        Duration = duration;
        Deadline = deadline;
        UserId = userId;
        TimeIntervals = timeIntervals;
    }

    public List<TimeInterval>? GetTimeIntervals() => TimeIntervals;
}