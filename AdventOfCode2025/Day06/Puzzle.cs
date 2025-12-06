namespace AdventOfCode2025.Day06;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 6;

    public override string Part1Solution()
    {
        var problems = ParseProblemsByColumnBlocks();
        var resultsSum = 0L;
        foreach (var (numbers, op) in problems)
        {
            resultsSum += op switch
            {
                '*' => numbers.Aggregate((a, b) => a * b),
                '+' => numbers.Aggregate((a, b) => a + b),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return resultsSum.ToString();
    }

    private List<(List<long> numbers, char op)> ParseProblemsByColumnBlocks()
    {
        var tokensGrid = Input.Select(SplitLineIntoTokens).ToList();
        if (tokensGrid.Count == 0)
        {
            return [];
        }

        var numRows = tokensGrid.Count;
        var numColumnBlocks = tokensGrid[0].Count;

        var problems = new List<(List<long> numbers, char op)>();

        for (var problemIdx = 0; problemIdx < numColumnBlocks; problemIdx++)
        {
            var numbers = new List<long>();
            for (var rowIdx = 0; rowIdx < numRows - 1; rowIdx++)
            {
                var number = tokensGrid[rowIdx][problemIdx];
                numbers.Add(long.Parse(number));
            }

            var op = char.Parse(tokensGrid[^1][problemIdx]);

            problems.Add((numbers, op));
        }

        return problems;
    }

    private static List<string> SplitLineIntoTokens(string line)
    {
        var tokenBuffer = "";
        var insideToken = false;
        var currentTokens = new List<string>();
        foreach (var c in line)
        {
            if (c == ' ')
            {
                if (insideToken)
                {
                    currentTokens.Add(tokenBuffer);
                    tokenBuffer = "";
                }

                insideToken = false;
            }
            else
            {
                tokenBuffer += c;
                insideToken = true;
            }
        }

        if (insideToken)
        {
            currentTokens.Add(tokenBuffer);
        }

        return currentTokens;
    }

    public override string Part2Solution()
    {
        var problems = ParseProblemsBySingleColumns();
        var resultsSum = 0L;
        foreach (var (numbers, op) in problems)
        {
            resultsSum += op switch
            {
                '*' => numbers.Aggregate((a, b) => a * b),
                '+' => numbers.Aggregate((a, b) => a + b),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return resultsSum.ToString();
    }

    private List<(List<long> numbers, char op)> ParseProblemsBySingleColumns()
    {
        if (Input.Length == 0)
        {
            return [];
        }

        var numRows = Input.Length;
        var numColumns = Input[0].Length;

        var problems = new List<(List<long> numbers, char op)>();

        var currentNumbers = new List<long>();
        for (var colIdx = numColumns - 1; colIdx >= 0; colIdx--)
        {
            var tokenBuffer = "";
            var insideToken = false;
            for (var rowIdx = 0; rowIdx < numRows; rowIdx++)
            {
                var c = Input[rowIdx][colIdx];
                if (char.IsDigit(c))
                {
                    tokenBuffer += c;
                    insideToken = true;
                }
                else if (c == ' ')
                {
                    if (!insideToken)
                    {
                        continue;
                    }

                    currentNumbers.Add(long.Parse(tokenBuffer));
                    tokenBuffer = "";
                    insideToken = false;
                }
                else
                {
                    if (tokenBuffer != "")
                    {
                        currentNumbers.Add(long.Parse(tokenBuffer));
                    }

                    problems.Add((currentNumbers, c));
                    currentNumbers = [];
                    break;
                }
            }
        }

        return problems;
    }
}