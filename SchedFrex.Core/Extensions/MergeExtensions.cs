using SchedFrex.Core.Models;

namespace SchedFrex.Core.Extensions;

public static class MergeExtensions
{
    public static List<Entry> MergeToList(this List<Entry> a, List<Entry> b)
    {
        List<Entry> result = [];

        int i = 0, j = 0;

        for (; i < a.Count && j < b.Count;)
        {
            if (a[i].Slot.End <= b[j].Slot.Start)
            {
                result.Add(a[i++]);
            }
            else if (b[j].Slot.End <= a[i].Slot.Start)
            {
                result.Add(b[j++]);
            }
            else throw new Exception("There ara intersected time intervals in merging collections!");
        }

        while (i < a.Count)
        {
            result.Add(a[i++]);
        }

        while (j < b.Count)
        {
            result.Add(b[j++]);
        }
        
        return result;
    }
}