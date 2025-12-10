using System.Text;

namespace AdventOfCode2025.Day09;

public class Grid
{
    public record Tile(int ColIdx, int RowIdx);

    public Grid(List<Tile> areaOutlineTiles)
    {
        _outlineTiles = areaOutlineTiles;
        _minCol = areaOutlineTiles.Min(t => t.ColIdx);
        _maxCol = areaOutlineTiles.Max(t => t.ColIdx);
        _minRow = areaOutlineTiles.Min(t => t.RowIdx);
        _maxRow = areaOutlineTiles.Max(t => t.RowIdx);
        _width = _maxCol - _minCol + 1;
        _height = _maxRow - _minRow + 1;

        _grid = new char[_width, _height];
    }

    private readonly List<Tile> _outlineTiles;

    private readonly int _minCol;
    private readonly int _maxCol;
    private readonly int _minRow;
    private readonly int _maxRow;

    private readonly int _width;
    private readonly int _height;

    private readonly char[,] _grid;

    public void SetAreaOutline()
    {
        for (var i = 0; i < _outlineTiles.Count; i++)
        {
            var tileA = _outlineTiles[i];
            var tileB = _outlineTiles[(i + 1) % _outlineTiles.Count];

            if (IsLineHorizontal(tileA, tileB))
            {
                var rowIdx = tileA.RowIdx;
                var minColIdx = Math.Min(tileA.ColIdx, tileB.ColIdx);
                var maxColIdx = Math.Max(tileA.ColIdx, tileB.ColIdx);
                for (var colIdx = minColIdx; colIdx <= maxColIdx; colIdx++)
                {
                    _grid[colIdx - _minCol, rowIdx - _minRow] = '#';
                }
            }
            else
            {
                var colIdx = tileA.ColIdx;
                var minRowIdx = Math.Min(tileA.RowIdx, tileB.RowIdx);
                var maxRowIdx = Math.Max(tileA.RowIdx, tileB.RowIdx);
                for (var rowIdx = minRowIdx; rowIdx <= maxRowIdx; rowIdx++)
                {
                    _grid[colIdx - _minCol, rowIdx - _minRow] = '#';
                }
            }

            continue;

            bool IsLineHorizontal(Tile a, Tile b)
            {
                return a.RowIdx == b.RowIdx;
            }
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var rowIdx = 0; rowIdx < _height; rowIdx++)
        {
            for (var colIdx = 0; colIdx < _width; colIdx++)
            {
                sb.Append(_grid[colIdx, rowIdx] != '\0'
                    ? _grid[colIdx, rowIdx]
                    : '.');
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}