using SchedFrex.Core.Models;

namespace SchedFrex.Application.Logic.SchedulerStrategies;

public interface ISchedulerStrategy
{
    public List<Entry>? Schedule(IReadOnlyList<Problem> tasks, Calendar calendar, TimeInterval planningRange);
}