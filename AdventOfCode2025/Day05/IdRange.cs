using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace AdventOfCode2025.Day05;

public record struct IdRange
{
    // Must always be true: Min <= Max
    public long Min;
    public long Max;

    public long Size => Max - Min + 1;

    public bool Contains(long id)
    {
        return id >= Min && id <= Max;
    }

    public override string ToString()
    {
        return "[" + Min + ".." + Max + "]";
    }

    public static List<IdRange> Merge(List<IdRange> nonOverlappingRanges, IdRange newIdRange)
    {
        var mergedRanges = nonOverlappingRanges;
        var rangeToMerge = newIdRange;
        while (true)
        {
            var overlapsDetected = false;
            var tempRanges = new List<IdRange>();
            foreach (var range in mergedRanges)
            {
                if (range.Overlaps(rangeToMerge))
                {
                    rangeToMerge = range.MergeOverlapping(rangeToMerge);
                    overlapsDetected = true;
                }
                else
                {
                    tempRanges.Add(range);
                }
            }

            mergedRanges = tempRanges;

            if (!overlapsDetected)
            {
                mergedRanges.Add(rangeToMerge);
                break;
            }
        }

        return mergedRanges;
    }

    [Pure]
    private IdRange MergeOverlapping(IdRange idRange)
    {
        Debug.Assert(idRange.Overlaps(idRange));
        return new IdRange
        {
            Min = Math.Min(Min, idRange.Min),
            Max = Math.Max(Max, idRange.Max)
        };
    }

    private bool Overlaps(IdRange idRange)
    {
        return Min <= idRange.Max && Max >= idRange.Min;
    }
}