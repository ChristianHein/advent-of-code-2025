using AdventOfCode2025;

namespace AdventOfCode2025.Day02;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 2;

    private List<(long start, long end)> ParseInput()
    {
        var line = Input[0];
        var idRanges = line.Split(',');
        return (from idRange in idRanges
            let start = idRange.Split('-')[0]
            let end = idRange.Split('-')[1]
            select (long.Parse(start), long.Parse(end))).ToList();
    }

    public override string Part1Solution()
    {
        var idRanges = ParseInput();
        var invalidsSum = 0L;
        
        foreach (var (start, end) in idRanges)
        {
            for (var id = start; id <= end; id++)
            {
                if (!IdValidityChecker.SimpleIsValid(id))
                {
                    invalidsSum += id;
                }
            }
        }

        return invalidsSum.ToString();
    }

    public override string Part2Solution()
    {
        var idRanges = ParseInput();
        var invalidsSum = 0L;
        
        foreach (var (start, end) in idRanges)
        {
            for (var id = start; id <= end; id++)
            {
                if (!IdValidityChecker.ComplexIsValid(id))
                {
                    invalidsSum += id;
                }
            }
        }

        return invalidsSum.ToString();
    }
}
