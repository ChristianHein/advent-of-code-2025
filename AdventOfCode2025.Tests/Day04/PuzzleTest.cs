using AdventOfCode2025.Day04;

namespace AdventOfCode2025.Tests.Day04;

[TestFixture]
[TestOf(typeof(Puzzle))]
public class PuzzleTest
{
    private readonly string[] _realInput = File.ReadAllLines("Day04/input");

    private readonly string[] _exampleInput =
    [
        "..@@.@@@@.",
        "@@@.@.@.@@",
        "@@@@@.@.@@",
        "@.@@@@..@.",
        "@@.@@@@.@@",
        ".@@@@@@@.@",
        ".@.@.@.@@@",
        "@.@@@.@@@@",
        ".@@@@@@@@.",
        "@.@.@@@.@."
    ];

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("1424"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("8727"));
    }

    [Test]
    public void UseExampleInput_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("13"));
    }

    [Test]
    public void UseExampleInput_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("43"));
    }
}