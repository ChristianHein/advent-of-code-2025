using System.Diagnostics;

namespace AdventOfCode2025;

using JetBrains.Annotations;

[PublicAPI]
public sealed class AdventOfCode2025
{
    public static void Main()
    {
        PrintDay2Solution();
    }

    public static void PrintDay2Solution()
    {
        var puzzle = new Day02.Puzzle(File.ReadAllLines("Day02/input"));
        Console.WriteLine(FormatDaySolutions(puzzle));
    }

    private static string FormatDaySolutions(BasePuzzle puzzle, bool printExecutionTimes = true)
    {
        var watch = Stopwatch.StartNew();
        var part1Solution = puzzle.Part1Solution();
        var part1ElapsedMs = watch.ElapsedMilliseconds;

        watch = Stopwatch.StartNew();
        var part2Solution = puzzle.Part2Solution();
        var part2ElapsedMs = watch.ElapsedMilliseconds;

        var output = $"Day {puzzle.Number:00}{Environment.NewLine}";

        output += $"  Part 1 solution: '{part1Solution}'{Environment.NewLine}";
        if (printExecutionTimes)
        {
            output += $"  (execution time: {part1ElapsedMs} ms){Environment.NewLine}";
        }

        output += $"  Part 2 solution: '{part2Solution}'{Environment.NewLine}";
        if (printExecutionTimes)
        {
            output += $"  (execution time: {part2ElapsedMs} ms){Environment.NewLine}";
        }

        return output;
    }
}
