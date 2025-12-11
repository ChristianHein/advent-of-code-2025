namespace AdventOfCode2025.Day11;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 11;

    private Dictionary<string, List<string>> ParseInput()
    {
        var deviceBindings = new Dictionary<string, List<string>>();
        foreach (var line in Input)
        {
            var deviceFrom = line.Split(": ", 2)[0];
            var devicesTo = line.Split(": ", 2)[1].Split(' ').ToList();

            if (deviceBindings.TryGetValue(deviceFrom, out var existingDeviceTo))
            {
                existingDeviceTo.Add(line);
            }
            else
            {
                deviceBindings.Add(deviceFrom, devicesTo);
            }
        }

        return deviceBindings;
    }

    public override string Part1Solution()
    {
        // Assumption: No binding loops.
        var deviceBindings = ParseInput();

        var stack = new Stack<string>();
        stack.Push("you");

        var numPaths = 0;

        while (stack.Count > 0)
        {
            var next = stack.Pop();
            if (next == "out")
            {
                numPaths++;
                continue;
            }

            deviceBindings[next].ForEach(x => stack.Push(x));
        }

        return numPaths.ToString();
    }

    private record PathNode(string Device, bool VisitedDac, bool VisitedFft, long NumPathsAcc);

    public override string Part2Solution()
    {
        // Assumption: No binding loops.
        var deviceBindings = ParseInput();

        var numPathsDict = new Dictionary<(string device, bool parentVisitedDac, bool parentVisitedFft), long>
        {
            { ("out", true, true), 1 },
            { ("out", false, true), 0 },
            { ("out", true, false), 0 },
            { ("out", false, false), 0 }
        };

        var stack = new Stack<PathNode>();
        stack.Push(new PathNode("svr", false, false, 0));

        while (stack.Count > 0)
        {
            var head = stack.Pop();

            if (numPathsDict.ContainsKey((head.Device, head.VisitedDac, head.VisitedFft)))
            {
                continue;
            }

            var visitedDac = head.VisitedDac || head.Device == "dac";
            var visitedFft = head.VisitedFft || head.Device == "fft";

            var nextDevices = deviceBindings[head.Device];
            if (nextDevices.Any(dev =>
                    !numPathsDict.ContainsKey((dev, visitedDac, visitedFft))))
            {
                stack.Push(head);
                var unvisitedDevice =
                    nextDevices.First(dev =>
                        !numPathsDict.ContainsKey((dev, visitedDac, visitedFft)));
                stack.Push(new PathNode(unvisitedDevice, visitedDac, visitedFft, head.NumPathsAcc));
            }
            else
            {
                numPathsDict.Add((head.Device, head.VisitedDac, head.VisitedFft),
                    head.NumPathsAcc + nextDevices.Sum(dev => numPathsDict[(dev, visitedDac, visitedFft)]));
            }
        }

        return numPathsDict[("svr", false, false)].ToString();
    }
}