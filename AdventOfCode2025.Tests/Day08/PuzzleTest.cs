using AdventOfCode2025.Day08;

namespace AdventOfCode2025.Tests.Day08;

[TestFixture]
[TestOf(typeof(Puzzle))]
public class PuzzleTest
{
    private readonly string[] _realInput = File.ReadAllLines("Day08/input");

    private readonly string[] _exampleInput =
    [
        "162,817,812",
        "57,618,57",
        "906,360,560",
        "592,479,940",
        "352,342,300",
        "466,668,158",
        "542,29,236",
        "431,825,988",
        "739,650,466",
        "52,470,668",
        "216,146,977",
        "819,987,18",
        "117,168,530",
        "805,96,715",
        "346,949,466",
        "970,615,88",
        "941,993,340",
        "862,61,35",
        "984,92,344",
        "425,690,689"
    ];

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("29406"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("7499461416"));
    }

    [Test]
    public void UseExampleInput_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput, 10);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("40"));
    }

    [Test]
    public void UseExampleInput_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("25272"));
    }
}