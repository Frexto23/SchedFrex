using SchedFrex.Core.Extensions;

namespace SchedFrex.Core.Models;

public class Calendar
{
    public Guid Id { get; }
    private List<Entry> _entries;

    public Calendar(Guid id, List<Entry> entries)
    {
        Id = id;
        _entries = entries;
    }

    public IReadOnlyList<Entry> GetEntries() => _entries.AsReadOnly();

    public List<TimeInterval> GetFreeSlots(TimeInterval planningRange)
    {
        if (_entries.Count == 0) return [planningRange];

        List<TimeInterval> result = [];

        var previousEnd = planningRange.Start;

        foreach (var e in _entries)
        {
            var inter = new TimeInterval(previousEnd, e.Slot.Start);
            if (inter.Correct()) result.Add(inter);
            previousEnd = e.Slot.End;
        }
            
        var after = new TimeInterval(_entries.Last().Slot.End, planningRange.End);
        if (after.Correct()) result.Add(after);
        
        return result;
    }

    public void InsertEntries(List<Entry> entries)
    {
        _entries = _entries.MergeToList(entries);
    }
}