namespace AdventOfCode2025.Day04;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 4;

    private struct Grid
    {
        public List<List<char>> Cells;
        public int RowsCount;
        public int ColsCount;

        public bool CellExists(int rowIdx, int colIdx)
        {
            return rowIdx >= 0 && rowIdx < RowsCount &&
                   colIdx >= 0 && colIdx < ColsCount;
        }
    }

    private Grid ParseInput()
    {
        return new Grid
        {
            Cells = Input.Select(line => line.ToCharArray().ToList()).ToList(),
            RowsCount = Input.Length,
            ColsCount = Input.Length > 0 ? Input[0].Length : 0
        };
    }

    public override string Part1Solution()
    {
        var grid = ParseInput();
        var totalRemovableRollsOfPaper = 0;
        for (var rowIdx = 0; rowIdx < grid.RowsCount; rowIdx++)
        {
            for (var colIdx = 0; colIdx < grid.Cells[rowIdx].Count; colIdx++)
            {
                if (grid.Cells[rowIdx][colIdx] == '@' &&
                    IsCellForkliftAccessible(grid, rowIdx, colIdx))
                {
                    totalRemovableRollsOfPaper++;
                }
            }
        }

        return totalRemovableRollsOfPaper.ToString();
    }

    private static bool IsCellForkliftAccessible(Grid grid, int rowIdx, int colIdx)
    {
        var countSurroundingRollsOfPaper = 0;

        if (grid.CellExists(rowIdx - 1, colIdx) && grid.Cells[rowIdx - 1][colIdx] == '@')
        {
            countSurroundingRollsOfPaper++;
        }

        if (grid.CellExists(rowIdx - 1, colIdx + 1) && grid.Cells[rowIdx - 1][colIdx + 1] == '@')
        {
            countSurroundingRollsOfPaper++;
        }

        if (grid.CellExists(rowIdx, colIdx + 1) && grid.Cells[rowIdx][colIdx + 1] == '@')
        {
            countSurroundingRollsOfPaper++;
        }

        if (grid.CellExists(rowIdx + 1, colIdx + 1) && grid.Cells[rowIdx + 1][colIdx + 1] == '@')
        {
            countSurroundingRollsOfPaper++;
        }

        if (grid.CellExists(rowIdx + 1, colIdx) && grid.Cells[rowIdx + 1][colIdx] == '@')
        {
            countSurroundingRollsOfPaper++;
        }

        if (grid.CellExists(rowIdx + 1, colIdx - 1) && grid.Cells[rowIdx + 1][colIdx - 1] == '@')
        {
            countSurroundingRollsOfPaper++;
        }

        if (grid.CellExists(rowIdx, colIdx - 1) && grid.Cells[rowIdx][colIdx - 1] == '@')
        {
            countSurroundingRollsOfPaper++;
        }

        if (grid.CellExists(rowIdx - 1, colIdx - 1) && grid.Cells[rowIdx - 1][colIdx - 1] == '@')
        {
            countSurroundingRollsOfPaper++;
        }

        return countSurroundingRollsOfPaper < 4;
    }

    public override string Part2Solution()
    {
        var grid = ParseInput();
        var totalRemovedRollsOfPaper = 0;
        int countRemovedInThisIteration;
        do
        {
            countRemovedInThisIteration = 0;
            for (var rowIdx = 0; rowIdx < grid.RowsCount; rowIdx++)
            {
                for (var colIdx = 0; colIdx < grid.Cells[rowIdx].Count; colIdx++)
                {
                    if (grid.Cells[rowIdx][colIdx] == '@' &&
                        IsCellForkliftAccessible(grid, rowIdx, colIdx))
                    {
                        grid.Cells[rowIdx][colIdx] = '.';
                        totalRemovedRollsOfPaper++;
                        countRemovedInThisIteration++;
                    }
                }
            }
        } while (countRemovedInThisIteration > 0);

        return totalRemovedRollsOfPaper.ToString();
    }
}