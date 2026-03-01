namespace SchedFrex.DataAccess.Entities;

public class ProblemEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public int Priority { get; set; }
    
    public TimeSpan Duration { get; set; }
    
    public List<TimeIntervalEntity>? TimeIntervals { get; set; } = [];
    public DateTime Deadline { get; set; }
    
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; } 
}