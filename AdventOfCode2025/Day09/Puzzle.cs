namespace AdventOfCode2025.Day09;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 9;

    private List<Tile> ParseInput()
    {
        return Input.Select(line =>
                new Tile(
                    int.Parse(line.Split(',', 2)[0]),
                    int.Parse(line.Split(',', 2)[1])))
            .ToList();
    }

    public override string Part1Solution()
    {
        var tiles = ParseInput();
        var largestArea = 0L;
        for (var i = 0; i < tiles.Count; i++)
        {
            for (var j = i + 1; j < tiles.Count; j++)
            {
                var tileA = tiles[i];
                var tileB = tiles[j];

                var area = (long)
                           (Math.Max(tileA.RowIdx, tileB.RowIdx) - Math.Min(tileA.RowIdx, tileB.RowIdx) + 1) *
                           (Math.Max(tileA.ColIdx, tileB.ColIdx) - Math.Min(tileA.ColIdx, tileB.ColIdx) + 1);
                largestArea = Math.Max(largestArea, area);
            }
        }

        return largestArea.ToString();
    }

    public override string Part2Solution()
    {
        var tiles = ParseInput();
        var largestArea = 0L;
        for (var i = 0; i < tiles.Count; i++)
        {
            for (var j = i + 1; j < tiles.Count; j++)
            {
                var tileA = tiles[i];
                var tileB = tiles[j];

                if (InsideAreaChecker.IsStrictlyInsideArea(tiles, tileA, tileB))
                {
                    var area = (long)
                               (Math.Max(tileA.RowIdx, tileB.RowIdx) - Math.Min(tileA.RowIdx, tileB.RowIdx) + 1) *
                               (Math.Max(tileA.ColIdx, tileB.ColIdx) - Math.Min(tileA.ColIdx, tileB.ColIdx) + 1);
                    largestArea = Math.Max(largestArea, area);
                }
            }
        }

        return largestArea.ToString();
    }
}