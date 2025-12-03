using System.Collections.Immutable;
using System.Diagnostics;

namespace AdventOfCode2025.Day03;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 3;

    public override string Part1Solution()
    {
        var totalJoltage = 0;
        foreach (var bank in Input)
        {
            Debug.Assert(bank is { Length: >= 2 });

            var digit1 = bank[..^1].Max();
            var digit1Idx = bank[..^1].IndexOf(digit1);

            var digit2 = bank[(digit1Idx + 1)..].Max();

            var bankJoltage = int.Parse(digit1.ToString() + digit2);
            totalJoltage += bankJoltage;
        }

        return totalJoltage.ToString();
    }

    public override string Part2Solution()
    {
        var totalJoltage = 0L;
        foreach (var bank in Input)
        {
            Debug.Assert(bank is { Length: >= 12 });

            var digit1 = bank[..^11].Max();
            var digit1Idx = bank[..^11].IndexOf(digit1);

            var digit2 = bank[(digit1Idx + 1)..^10].Max();
            var digit2Idx = bank[(digit1Idx + 1)..^10].IndexOf(digit2) + digit1Idx + 1;

            var digit3 = bank[(digit2Idx + 1)..^9].Max();
            var digit3Idx = bank[(digit2Idx + 1)..^9].IndexOf(digit3) + digit2Idx + 1;

            var digit4 = bank[(digit3Idx + 1)..^8].Max();
            var digit4Idx = bank[(digit3Idx + 1)..^8].IndexOf(digit4) + digit3Idx + 1;

            var digit5 = bank[(digit4Idx + 1)..^7].Max();
            var digit5Idx = bank[(digit4Idx + 1)..^7].IndexOf(digit5) + digit4Idx + 1;

            var digit6 = bank[(digit5Idx + 1)..^6].Max();
            var digit6Idx = bank[(digit5Idx + 1)..^6].IndexOf(digit6) + digit5Idx + 1;

            var digit7 = bank[(digit6Idx + 1)..^5].Max();
            var digit7Idx = bank[(digit6Idx + 1)..^5].IndexOf(digit7) + digit6Idx + 1;

            var digit8 = bank[(digit7Idx + 1)..^4].Max();
            var digit8Idx = bank[(digit7Idx + 1)..^4].IndexOf(digit8) + digit7Idx + 1;

            var digit9 = bank[(digit8Idx + 1)..^3].Max();
            var digit9Idx = bank[(digit8Idx + 1)..^3].IndexOf(digit9) + digit8Idx + 1;

            var digit10 = bank[(digit9Idx + 1)..^2].Max();
            var digit10Idx = bank[(digit9Idx + 1)..^2].IndexOf(digit10) + digit9Idx + 1;

            var digit11 = bank[(digit10Idx + 1)..^1].Max();
            var digit11Idx = bank[(digit10Idx + 1)..^1].IndexOf(digit11) + digit10Idx + 1;

            var digit12 = bank[(digit11Idx + 1)..].Max();

            var bankJoltage = long.Parse(digit1.ToString() + digit2 + digit3 + digit4 + digit5 + digit6 + digit7 +
                                         digit8 + digit9 + digit10 + digit11 + digit12);
            totalJoltage += bankJoltage;
        }

        return totalJoltage.ToString();
    }
}