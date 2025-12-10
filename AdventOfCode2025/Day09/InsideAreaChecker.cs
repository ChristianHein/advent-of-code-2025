namespace AdventOfCode2025.Day09;

public static class InsideAreaChecker
{
    public static bool IsStrictlyInsideArea(List<Tile> tiles, Tile tileA, Tile tileB)
    {
        var (leftAreaTile, rightAreaTile) = tileA.ColIdx <= tileB.ColIdx
            ? (tileA, tileB)
            : (tileB, tileA);
        var (upperAreaTile, lowerAreaTile) = tileA.RowIdx <= tileB.RowIdx
            ? (tileA, tileB)
            : (tileB, tileA);

        for (var i = 0; i < tiles.Count; i++)
        {
            var lineTile1 = tiles[i];
            var lineTile2 = tiles[(i + 1) % tiles.Count];

            if (IsLineHorizontal(lineTile1, lineTile2))
            {
                var (leftLineTile, rightLineTile) = lineTile1.ColIdx <= lineTile2.ColIdx
                    ? (lineTile1, lineTile2)
                    : (lineTile2, lineTile1);

                var rowIdx = leftLineTile.RowIdx;
                var isWithinRowBounds = upperAreaTile.RowIdx < rowIdx && rowIdx < lowerAreaTile.RowIdx;
                var crossesLeftSide = leftLineTile.ColIdx <= leftAreaTile.ColIdx &&
                                      rightLineTile.ColIdx > leftAreaTile.ColIdx;
                var crossesRightSide = leftLineTile.ColIdx < rightAreaTile.ColIdx &&
                                       rightLineTile.ColIdx >= rightAreaTile.ColIdx;

                if (isWithinRowBounds && (crossesLeftSide || crossesRightSide))
                {
                    return false;
                }
            }
            else
            {
                var (upperLineTile, lowerLineTile) = lineTile1.RowIdx <= lineTile2.RowIdx
                    ? (lineTile1, lineTile2)
                    : (lineTile2, lineTile1);

                var colIdx = lowerLineTile.ColIdx;
                var isWithinColBounds = leftAreaTile.ColIdx < colIdx && colIdx < rightAreaTile.ColIdx;
                var crossesTopSide = upperLineTile.RowIdx <= upperAreaTile.RowIdx &&
                                     lowerLineTile.RowIdx > upperAreaTile.RowIdx;
                var crossesBottomSide = upperLineTile.RowIdx < lowerAreaTile.RowIdx &&
                                        lowerLineTile.RowIdx >= lowerAreaTile.RowIdx;

                if (isWithinColBounds && (crossesTopSide || crossesBottomSide))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool IsLineHorizontal(Tile tileA, Tile tileB)
    {
        return tileA.RowIdx == tileB.RowIdx;
    }
}