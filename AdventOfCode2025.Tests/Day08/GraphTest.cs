using AdventOfCode2025.Day08;

namespace AdventOfCode2025.Tests.Day08;

[TestFixture]
[TestOf(typeof(Graph))]
public class GraphTest
{
    [Test]
    public void ConnectOnce()
    {
        var graph = new Graph();
        graph.AddNode(0, 0, 1);
        graph.AddNode(0, 0, 10);
        graph.AddNode(0, 0, 100);

        graph.TryConnectTwoNodesWithShortestDistance();

        Assert.That(graph.GetCircuits(), Is.EquivalentTo(
            new List<List<Graph.Node>>
            {
                new() { new Graph.Node(0, 0, 1), new Graph.Node(0, 0, 10) },
                new() { new Graph.Node(0, 0, 100) }
            }));
    }

    [Test]
    public void ConnectTwice()
    {
        var graph = new Graph();
        graph.AddNode(0, 0, 1);
        graph.AddNode(0, 0, 10);
        graph.AddNode(0, 0, 100);

        graph.TryConnectTwoNodesWithShortestDistance();
        graph.TryConnectTwoNodesWithShortestDistance();

        Assert.That(graph.GetCircuits(), Is.EquivalentTo(
            new List<List<Graph.Node>>
            {
                new() { new Graph.Node(0, 0, 1), new Graph.Node(0, 0, 10), new Graph.Node(0, 0, 100) }
            }));
    }

    [Test]
    public void ConnectMoreOftenThanPossible_AllNodesAreInSameCircuit()
    {
        var graph = new Graph();
        graph.AddNode(0, 0, 1);
        graph.AddNode(0, 0, 10);
        graph.AddNode(0, 0, 100);

        graph.TryConnectTwoNodesWithShortestDistance();
        graph.TryConnectTwoNodesWithShortestDistance();
        graph.TryConnectTwoNodesWithShortestDistance();

        Assert.That(graph.GetCircuits(), Is.EquivalentTo(
            new List<List<Graph.Node>>
            {
                new() { new Graph.Node(0, 0, 1), new Graph.Node(0, 0, 10), new Graph.Node(0, 0, 100) }
            }));
    }

    [Test]
    public void Test()
    {
        var graph = new Graph();
        graph.AddNode(0, 0, 1);
        graph.AddNode(0, 0, 10);
        graph.AddNode(0, 1, 10);
        graph.AddNode(2, 0, 10);
        graph.AddNode(0, 0, 100);

        graph.TryConnectTwoNodesWithShortestDistance();
        graph.TryConnectTwoNodesWithShortestDistance();

        Assert.That(graph.GetCircuits(), Is.EquivalentTo(
            new List<List<Graph.Node>>
            {
                new() { new Graph.Node(0, 0, 1) },
                new() { new Graph.Node(0, 0, 10), new Graph.Node(2, 0, 10), new Graph.Node(0, 1, 10) },
                new() { new Graph.Node(0, 0, 100) }
            }));
    }

    [Test]
    public void GetCircuitsTest()
    {
        var graph = new Graph();
        var nodeA = new Graph.Node(0, 0, 1);
        var nodeB = new Graph.Node(0, 0, 10);
        var nodeC = new Graph.Node(0, 0, 100);
        var nodeD = new Graph.Node(0, 0, 1_000);
        var nodeE = new Graph.Node(0, 0, 1_001);

        graph.AddNode(nodeA);
        graph.AddNode(nodeB);
        graph.AddNode(nodeC);
        graph.AddNode(nodeD);
        graph.AddNode(nodeE);

        graph.TryConnectTwoNodesWithShortestDistance();
        graph.TryConnectTwoNodesWithShortestDistance();

        Assert.Multiple(() =>
        {
            Assert.That(graph.GetCircuitOfNode(nodeA), Is.EquivalentTo(
                new List<Graph.Node> { nodeA, nodeB }));
            Assert.That(graph.GetCircuitOfNode(nodeB), Is.EquivalentTo(
                new List<Graph.Node> { nodeA, nodeB }));
            Assert.That(graph.GetCircuitOfNode(nodeC), Is.EquivalentTo(
                new List<Graph.Node> { nodeC }));
            Assert.That(graph.GetCircuitOfNode(nodeD), Is.EquivalentTo(
                new List<Graph.Node> { nodeD, nodeE }));
            Assert.That(graph.GetCircuitOfNode(nodeE), Is.EquivalentTo(
                new List<Graph.Node> { nodeD, nodeE }));
        });
    }
}