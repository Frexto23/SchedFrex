using SchedFrex.Core.Models;

namespace SchedFrex.Application.Logic.SchedulerStrategies;

public class GreedyScheduler : ISchedulerStrategy
{
    private static TimeInterval? FitTaskInSlots(Problem task, List<TimeInterval> slots)
    {
        foreach (var slot in slots)
        {
            foreach (var line in task.GetTimeIntervals()!)
            {
                var inter = slot.IntersectWithDuration(line, task.Duration);
                if (inter == null) continue;

                return new TimeInterval(inter.Value.Start,
                    inter.Value.Start + task.Duration);
            }
        }

        return null;
    }
    
    public List<Entry> Schedule(IReadOnlyList<Problem> tasks, Calendar calendar, TimeInterval planningRange)
    {
        List<Entry> schedule = [];
        
        var sortedTasks = tasks
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.Deadline)
            .ToList();

        var slots = calendar.GetFreeSlots(planningRange);

        // не учитывает повторения задач 
        foreach (var task in sortedTasks)
        {
            var assignedSlot = FitTaskInSlots(task, slots);
            if (assignedSlot == null) continue;
            
            schedule.Add(new Entry(task, assignedSlot.Value));

            var newSlots = slots.SelectMany(s =>
            {
                List<TimeInterval> res = [];
                var cut = s.Divide(assignedSlot.Value);
                if (cut.left != null) res.Add(cut.left.Value);
                if (cut.right != null) res.Add(cut.right.Value);
                return res;
            }).ToList();
            slots = newSlots;
        }

        return schedule
            .OrderBy(ti => ti.Slot.Start)
            .ToList();
    }
}