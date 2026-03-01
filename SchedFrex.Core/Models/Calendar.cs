namespace SchedFrex.Core.Models;

public class Calendar
{
    public Guid Id { get; }
    public List<Entry> Events { get; } = [];

    public List<TimeInterval> GetFreeSlots(TimeInterval planningRange)
    {
        if (Events.Count == 0) return [planningRange];

        List<TimeInterval> result = [];

        var previousEnd = planningRange.Start;

        foreach (var e in Events)
        {
            var inter = new TimeInterval(previousEnd, e.Slot.Start);
            if (inter.Correct()) result.Add(inter);
            previousEnd = e.Slot.End;
        }
            
        var after = new TimeInterval(Events.Last().Slot.End, planningRange.End);
        if (after.Correct()) result.Add(after);
        
        return result;
    }
}