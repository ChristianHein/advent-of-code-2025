namespace AdventOfCode2025.Day05;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 5;

    private (List<IdRange> freshRanges, List<long> idsToCheck) ParseInput()
    {
        var freshRanges = new List<IdRange>();
        var i = 0;
        while (Input[i] != "")
        {
            freshRanges.Add(new IdRange
            {
                Min = long.Parse(Input[i].Split('-', 2)[0]),
                Max = long.Parse(Input[i].Split('-', 2)[1])
            });

            i++;
        }

        i++;

        var idsToCheck = new List<long>();
        while (i < Input.Length)
        {
            idsToCheck.Add(long.Parse(Input[i]));
            i++;
        }

        return (freshRanges, idsToCheck);
    }

    public override string Part1Solution()
    {
        var (freshRanges, idsToCheck) = ParseInput();
        var countFreshIds = idsToCheck.Count(id => IsFresh(freshRanges, id));
        return countFreshIds.ToString();
    }

    private static bool IsFresh(List<IdRange> freshRanges, long id)
    {
        return freshRanges.Any(range => range.Contains(id));
    }

    public override string Part2Solution()
    {
        var (inputRanges, _) = ParseInput();

        var mergedRanges = new List<IdRange>();
        mergedRanges = inputRanges.Aggregate(mergedRanges, IdRange.Merge);

        var totalNumbersInRanges = mergedRanges.Sum(range => range.Size);
        return totalNumbersInRanges.ToString();
    }
}