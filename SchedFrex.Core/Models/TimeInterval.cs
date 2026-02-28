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

    public TimeInterval? IntersectWithDuration(TimeInterval other, TimeSpan durationOther)
    {
        var res = new TimeInterval()
        {
            Start = Start > other.Start ? Start : other.Start,
            End = End - durationOther < other.End ? End - durationOther : other.End
        };
        return res.Correct() ? res : null;
    }

    public (TimeInterval? left, TimeInterval? right) Divide(TimeInterval separator)
    {
        var l = new TimeInterval(Start, separator.Start);
        var r = new TimeInterval(separator.End, End);
        return (Start < separator.Start ? l : null, separator.End < End ? r : null);
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