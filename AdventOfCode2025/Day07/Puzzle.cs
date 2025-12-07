using System.Diagnostics;
using System.Text;

namespace AdventOfCode2025.Day07;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 7;

    private struct Grid
    {
        public List<List<char>> Cells;
        public int RowCount;
        public int ColCount;

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var row in Cells)
            {
                foreach (var cell in row)
                {
                    sb.Append(cell);
                }

                sb.Append('\n');
            }

            return sb.ToString();
        }
    }

    private Grid ParseInput()
    {
        return new Grid
        {
            Cells = Input.Select(line => line.ToCharArray().ToList()).ToList(),
            RowCount = Input.Length,
            ColCount = Input.Length > 0 ? Input[0].Length : 0
        };
    }

    public override string Part1Solution()
    {
        var grid = ParseInput();

        // Assumption: Only one start
        var startColIdx = grid.Cells[0].IndexOf('S');
        Debug.Assert(startColIdx != -1);

        grid.Cells[1][startColIdx] = '|';

        var totalSplits = 0;
        for (var rowIdx = 1; rowIdx < grid.RowCount - 1; rowIdx++)
        {
            for (var colIdx = 0; colIdx < grid.ColCount; colIdx++)
            {
                var row = grid.Cells[rowIdx];
                var rowBelow = grid.Cells[rowIdx + 1];

                switch (row[colIdx])
                {
                    case '|' when rowBelow[colIdx] == '.':
                        rowBelow[colIdx] = '|';
                        break;
                    case '|' when rowBelow[colIdx] == '^':
                        rowBelow[colIdx - 1] = '|';
                        rowBelow[colIdx + 1] = '|';

                        totalSplits++;
                        break;
                }
            }
        }

        return totalSplits.ToString();
    }

    public override string Part2Solution()
    {
        var grid = ParseInput();

        // Assumption: Only one start
        var startColIdx = grid.Cells[0].IndexOf('S');
        Debug.Assert(startColIdx != -1);

        var stack = new Stack<(int rowIdx, int colIdx)>();
        stack.Push((1, startColIdx));

        var reachableTimelines = new Dictionary<(int rowIdx, int colIdx), long>();

        while (stack.Count > 0)
        {
            var (rowIdx, colIdx) = stack.Peek();

            if (rowIdx == grid.RowCount)
            {
                reachableTimelines.Add((rowIdx - 1, colIdx), 1L);
                stack.Pop();
                continue;
            }

            switch (grid.Cells[rowIdx][colIdx])
            {
                case '.':
                    if (reachableTimelines.ContainsKey((rowIdx, colIdx)))
                    {
                        reachableTimelines.Add((rowIdx - 1, colIdx), reachableTimelines[(rowIdx, colIdx)]);
                        stack.Pop();
                    }
                    else
                    {
                        grid.Cells[rowIdx][colIdx] = '|';
                        stack.Push((rowIdx + 1, colIdx));
                    }

                    break;
                case '^':
                    if (reachableTimelines.ContainsKey((rowIdx, colIdx - 1)) &&
                        reachableTimelines.ContainsKey((rowIdx, colIdx + 1)))
                    {
                        reachableTimelines.Add((rowIdx - 1, colIdx), reachableTimelines[(rowIdx, colIdx - 1)] +
                                                                     reachableTimelines[(rowIdx, colIdx + 1)]);
                        stack.Pop();
                    }
                    else
                    {
                        if (!reachableTimelines.ContainsKey((rowIdx, colIdx - 1)))
                        {
                            stack.Push((rowIdx, colIdx - 1));
                        }

                        if (!reachableTimelines.ContainsKey((rowIdx, colIdx + 1)))
                        {
                            stack.Push((rowIdx, colIdx + 1));
                        }
                    }

                    break;
                case '|':
                    reachableTimelines.Add((rowIdx - 1, colIdx), reachableTimelines[(rowIdx, colIdx)]);
                    grid.Cells[rowIdx][colIdx] = '.';
                    stack.Pop();
                    break;
            }
        }

        return reachableTimelines[(1, startColIdx)].ToString();
    }
}