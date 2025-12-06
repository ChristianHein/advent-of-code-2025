using AdventOfCode2025.Day06;

namespace AdventOfCode2025.Tests.Day06;

[TestFixture]
[TestOf(typeof(Puzzle))]
public class PuzzleTest
{
    private readonly string[] _realInput = File.ReadAllLines("Day06/input");

    private readonly string[] _exampleInput =
    [
        "123 328  51 64 ",
        " 45 64  387 23 ",
        "  6 98  215 314",
        "*   +   *   +  ",
    ];

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("5977759036837"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("9630000828442"));
    }

    [Test]
    public void UseExampleInput_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("4277556"));
    }

    [Test]
    public void UseExampleInput_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("3263827"));
    }
}