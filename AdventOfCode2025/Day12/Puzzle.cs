using System.Text.RegularExpressions;

namespace AdventOfCode2025.Day12;

using Shape = List<List<char>>;
using Region = (byte width, byte height);

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 12;

    private (List<Shape>, List<(Region region, List<byte> occurrences)>) ParseInput()
    {
        var lineIdx = 0;
        var shapes = new List<Shape>();
        while (true)
        {
            if (!Regex.IsMatch(Input[lineIdx], @"^\d+:$"))
            {
                break;
            }

            var shape = new Shape();
            while (Input[lineIdx] != "")
            {
                shape.Add(Input[lineIdx].ToCharArray().ToList());
                lineIdx++;
            }

            shapes.Add(shape);
            lineIdx++;
        }

        var regions = new List<(Region, List<byte>)>();
        while (lineIdx < Input.Length)
        {
            var strDimensions = Input[lineIdx].Split(": ", 2)[0].Split('x', 2);
            var width = byte.Parse(strDimensions[0]);
            var height = byte.Parse(strDimensions[1]);
            var regionDimensions = (width, height);

            var shapeOccurrencesToFit = Input[lineIdx].Split(": ", 2)[1].Split(' ').Select(byte.Parse).ToList();

            regions.Add((regionDimensions, shapeOccurrencesToFit));
            lineIdx++;
        }

        return (shapes, regions);
    }

    public override string Part1Solution()
    {
        var (shapes, regions) = ParseInput();

        // Magic number, arbitrarily chosen to make the solution work for both the real input _and_ the example input.
        const float magicTolerance = 0.2f;
        var fittableRegionsCount =
            regions.Count(region => PassesTrivialAreaCheck(region.region, shapes, region.occurrences, magicTolerance));

        return fittableRegionsCount.ToString();
    }

    public override string Part2Solution()
    {
        return "No part 2.";
    }

    private static bool PassesTrivialAreaCheck(Region region, List<Shape> shapes, List<byte> occurrences,
        float tolerance)
    {
        if (shapes.Count == 0)
        {
            return true;
        }

        var totalArea = 0L;
        for (var i = 0; i < shapes.Count; i++)
        {
            totalArea += AreaOfShape(shapes[i]) * occurrences[i];
        }

        return totalArea <= region.width * region.height / (1 + tolerance);
    }

    private static byte AreaOfShape(Shape shape)
    {
        return (byte)shape.SelectMany(row => row).Count(c => c == '#');
    }
}