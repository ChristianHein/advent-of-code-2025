using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace AdventOfCode2025.Day05;

public class RangeMerger
{
    public record struct Range
    {
        // Must always be true: Min <= Max
        public long Min;
        public long Max;

        public long Size => Max - Min + 1;

        public bool Contains(long id)
        {
            return id >= Min && id <= Max;
        }

        public bool Overlaps(Range range)
        {
            return Min <= range.Max && Max >= range.Min;
        }

        [Pure]
        public Range MergeOverlapping(Range range)
        {
            Debug.Assert(range.Overlaps(range));
            return new Range
            {
                Min = Math.Min(Min, range.Min),
                Max = Math.Max(Max, range.Max)
            };
        }

        public override string ToString()
        {
            return "[" + Min + ".." + Max + "]";
        }
    }

    // Assumption: `ranges` contains no overlapping ranges.
    public static List<Range> Merge(List<Range> ranges, Range newRange)
    {
        var mergedRanges = ranges;
        var rangeToMerge = newRange;
        while (true)
        {
            var overlapsDetected = false;
            var tempRanges = new List<Range>();
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
}