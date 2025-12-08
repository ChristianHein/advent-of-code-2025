namespace AdventOfCode2025.Day08;

public class Puzzle(string[] input, int connectionAttemptsForPart1 = 1000) : BasePuzzle(input)
{
    public override int Number => 8;

    private Graph ParseInput()
    {
        var graph = new Graph();
        foreach (var line in Input)
        {
            var coords = line.Split(',', 3);
            graph.AddNode(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2]));
        }

        return graph;
    }

    public override string Part1Solution()
    {
        var graph = ParseInput();

        var connectionAttempts = connectionAttemptsForPart1;
        while (connectionAttempts > 0)
        {
            _ = graph.TryConnectTwoNodesWithShortestDistance();
            connectionAttempts--;
        }

        var circuits = graph.GetCircuits();
        circuits.Sort((a, b) => b.Count - a.Count);

        return circuits.Count < 3
            ? "0"
            : (circuits[0].Count * circuits[1].Count * circuits[2].Count).ToString();
    }

    public override string Part2Solution()
    {
        var graph = ParseInput();

        (Graph.Node, Graph.Node)? lastConnectedNodes = null;
        while (graph.GetCircuits().Count > 1)
        {
            var connectedNodes = graph.TryConnectTwoNodesWithShortestDistance();
            if (connectedNodes != null)
            {
                lastConnectedNodes = connectedNodes.Value;
            }
        }

        if (lastConnectedNodes == null)
        {
            return "0";
        }

        var (nodeA, nodeB) = lastConnectedNodes.Value;

        return ((long)nodeA.X * nodeB.X).ToString();
    }
}