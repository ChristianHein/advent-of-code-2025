namespace AdventOfCode2025.Day09;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 9;

    private List<Grid.Tile> ParseInput()
    {
        return Input.Select(line =>
                new Grid.Tile(
                    int.Parse(line.Split(',', 2)[0]),
                    int.Parse(line.Split(',', 2)[1])))
            .ToList();
    }

    public override string Part1Solution()
    {
        var tiles = ParseInput();
        var largestArea = 0L;
        foreach (var tileA in tiles)
        {
            foreach (var tileB in tiles)
            {
                var area = (long)(Math.Max(tileA.RowIdx, tileB.RowIdx) - Math.Min(tileA.RowIdx, tileB.RowIdx) + 1) *
                           (Math.Max(tileA.ColIdx, tileB.ColIdx) - Math.Min(tileA.ColIdx, tileB.ColIdx) + 1);
                largestArea = Math.Max(largestArea, area);
            }
        }

        return largestArea.ToString();
    }

    public override string Part2Solution()
    {
        var tiles = ParseInput();
        var grid = new Grid(tiles);

        grid.SetAreaOutline();
        Console.WriteLine(grid.ToString());

        throw new NotImplementedException();
    }
}