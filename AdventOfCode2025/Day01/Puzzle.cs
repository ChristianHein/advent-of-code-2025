namespace AdventOfCode2025.Day01;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 1;

    public override string Part1Solution()
    {
        var dial = 50;
        var zeros = 0;
        foreach (var rotation in Input)
        {
            var direction = rotation[0];
            var distance = int.Parse(rotation[1..]);

            dial = direction switch
            {
                'L' => (dial + distance) % 100,
                'R' => (dial - distance + 100) % 100,
                _ => throw new ArgumentOutOfRangeException(nameof(rotation), rotation, null)
            };

            if (dial == 0)
            {
                zeros++;
            }
        }

        return zeros.ToString();
    }

    public override string Part2Solution()
    {
        var dial = 50;
        var clicks = 0;
        foreach (var rotation in Input)
        {
            var direction = rotation[0];
            var distance = int.Parse(rotation[1..]);

            // Nothing clever - just simulate every single unit of rotation and check if zero
            foreach (var _ in Enumerable.Range(0, distance))
            {
                dial = direction switch
                {
                    'L' => (dial + 1) % 100,
                    'R' => (dial - 1 + 100) % 100,
                    _ => throw new ArgumentOutOfRangeException()
                };

                if (dial == 0)
                {
                    clicks++;
                }
            }
        }

        return clicks.ToString();
    }
}