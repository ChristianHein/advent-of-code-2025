using AdventOfCode2025.Day03;

namespace AdventOfCode2025.Tests.Day03;

[TestFixture]
[TestOf(typeof(Puzzle))]
public class PuzzleTest
{
    private readonly string[] _realInput = File.ReadAllLines("Day03/input");

    private readonly string[] _exampleInput =
    [
        "987654321111111",
        "811111111111119",
        "234234234234278",
        "818181911112111"
    ];

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("17311"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("171419245422055"));
    }

    [Test]
    public void UseExampleInput_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("357"));
    }

    [Test]
    public void UseExampleInput_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("3121910778619"));
    }
}