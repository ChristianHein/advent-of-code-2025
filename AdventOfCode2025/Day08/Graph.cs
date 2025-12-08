using System.Diagnostics;

namespace AdventOfCode2025.Day08;

public class Graph
{
    public record Node(int X, int Y, int Z)
    {
        public static double Distance(Node a, Node b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2) + Math.Pow(a.Z - b.Z, 2));
        }

        public override string ToString() => $"{X},{Y},{Z}";
    }

    private readonly Dictionary<Node, List<Node>> _adjacencies = new();
    private readonly SortedDictionary<double, (Node, Node)> _shortestDistances = new();

    private List<HashSet<Node>>? _circuitsCache = [];
    private Dictionary<Node, HashSet<Node>>? _nodeToCircuitCache = [];

    public void AddNode(int x, int y, int z)
    {
        AddNode(new Node(x, y, z));
    }

    public void AddNode(Node node)
    {
        // Assumption: A node is never added twice
        Debug.Assert(!_adjacencies.ContainsKey(node));

        foreach (var oldNode in _adjacencies.Keys)
        {
            // Assumption: Puzzle input never includes two nodes with exact same distance to each other
            Debug.Assert(!_shortestDistances.ContainsKey(Node.Distance(node, oldNode)));
            _shortestDistances.Add(Node.Distance(node, oldNode), (node, oldNode));
        }

        _adjacencies.Add(node, []);
        _circuitsCache?.Add([node]);
        _nodeToCircuitCache?.Add(node, [node]);
    }

    private void AddConnection(Node a, Node b)
    {
        // Assumption: Nodes are not already connected
        _adjacencies[a].Add(b);
        _adjacencies[b].Add(a);
        _circuitsCache = null;
        _nodeToCircuitCache = null;
    }

    public List<HashSet<Node>> GetCircuits()
    {
        if (_circuitsCache != null)
        {
            return _circuitsCache;
        }

        var circuits = new List<HashSet<Node>>();
        var checkedNodes = new HashSet<Node>();
        foreach (var node in _adjacencies.Keys)
        {
            if (!checkedNodes.Contains(node))
            {
                var circuit = GetCircuitOfNode(node);
                circuits.Add(circuit);

                foreach (var circuitNode in circuit)
                {
                    checkedNodes.Add(circuitNode);
                }
            }
        }

        _circuitsCache = circuits;
        return circuits;
    }

    public HashSet<Node> GetCircuitOfNode(Node node)
    {
        if (_nodeToCircuitCache != null && _nodeToCircuitCache.TryGetValue(node, out var cachedCircuit))
        {
            return cachedCircuit;
        }

        var inputNode = node;

        var circuit = new HashSet<Node>();
        var stack = new Stack<Node>();
        stack.Push(node);
        while (stack.Count != 0)
        {
            node = stack.Pop();
            circuit.Add(node);
            foreach (var connectedNode in _adjacencies[node])
            {
                if (!circuit.Contains(connectedNode))
                {
                    stack.Push(connectedNode);
                }
            }
        }

        _nodeToCircuitCache ??= new Dictionary<Node, HashSet<Node>>();
        _nodeToCircuitCache.Add(inputNode, circuit);
        return circuit;
    }

    private bool DoNodesShareCircuit(Node a, Node b)
    {
        return GetCircuitOfNode(a).Contains(b);
    }

    public (Node, Node)? TryConnectTwoNodesWithShortestDistance()
    {
        if (_shortestDistances.Keys.Count == 0)
        {
            return null;
        }

        foreach (var (_, (leftNode, rightNode)) in _shortestDistances)
        {
            if (DoNodesShareCircuit(leftNode, rightNode))
            {
                _shortestDistances.Remove(_shortestDistances.Keys.First());
                return null;
            }

            AddConnection(leftNode, rightNode);
            _shortestDistances.Remove(_shortestDistances.Keys.First());
            return (leftNode, rightNode);
        }

        return null;
    }
}