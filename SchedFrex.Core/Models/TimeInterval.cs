namespace SchedFrex.Core.Models;

public readonly record struct TimeInterval
{
    public DateTime Start { get; private init; }
    public DateTime End { get; private init; }

    public TimeInterval(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public TimeInterval? Intersect(TimeInterval other)
    {
        TimeInterval intersection = new()
        {
            Start = Start > other.Start ? Start : other.Start,
            End = End < other.End ? End : other.End,
        };
        return intersection.Correct() ? intersection : null;
    }
    
    /// <summary>
    /// Ищет пересечение слота и старт тайма задачи
    /// </summary>
    /// <param name="other">слот</param>
    /// <param name="durationOther">Возможное время начала задачи</param>
    /// <returns>Возвращает возможное время начала задачи в этом слоте или null, если это невозможно</returns>
    public TimeInterval? IntersectWithDuration(TimeInterval other, TimeSpan durationOther)
    {
        var res = new TimeInterval()
        {
            Start = Start > other.Start ? Start : other.Start,
            End = End - durationOther < other.End ? End - durationOther : other.End
        };
        return res.Correct() ? res : null;
    }

    /// <summary>
    /// Разделяет слот на две части выбранной задачей
    /// </summary>
    /// <param name="separator">Разделитель</param>
    /// <returns>Nullable левая и правая части соотвественно</returns>
    public (TimeInterval? left, TimeInterval? right) Divide(TimeInterval separator)
    {
        TimeInterval? l = null;
        TimeInterval? r = null;
        if (Start < separator.Start && separator.Start <= End)
        {
            l = new TimeInterval(Start, separator.Start);
        }
        if (separator.End < End && Start <= separator.End)
        {
            r = new TimeInterval(separator.End, End);
        }

        return (l, r);
    }

    public TimeSpan Length()
    {
        return End - Start;
    }

    public bool Correct()
    {
        return Start <= End;
    }
}