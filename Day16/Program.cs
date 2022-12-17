using Dijkstra.NET.Graph.Simple;
using Dijkstra.NET.ShortestPath;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        string assemblyPath = Assembly.GetExecutingAssembly().Location;
        string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
#if DEBUG
string textPath = Path.Combine(assemblyDirectory, "input.debug.txt");
#else
        string textPath = Path.Combine(assemblyDirectory, "input.txt");
#endif
        Graph graph = new Graph();

        var input = File.ReadAllLines(textPath)
            .Select(line =>
                Regex.Match(line, @"Valve (\w+) has flow rate=(\d+); tunnels? leads? to valves? ([\w, ]+)"))
            .Select((match) => (Node: graph.AddNode(), Name: match.Groups[1].Value, Flow: int.Parse(match.Groups[2].Value),
                Tunnels: match.Groups[3].Value.Split(", ")))
            .ToDictionary(valve => valve.Name, valve => valve);

        var valves = input.Values
            .Select(valve => (valve.Node, valve.Name, valve.Flow, Tunnels: valve.Tunnels.Select(s => input[s].Node).ToList()))
            .ToDictionary(valve => valve.Node, valve => valve);

        foreach (var (from, _, _, tunnels) in valves.Values)
        {
            foreach (var to in tunnels)
            {
                graph.Connect(from, to, 1);
            }
        }

        var pathLengths = new Dictionary<(uint, uint), int>();
        (int T, int Score) Move(uint from, uint to, (int T, int Score) previous)
        {
            if (!pathLengths.TryGetValue((from, to), out var dist))
            {
                dist = graph.Dijkstra(from, to).Distance;
                pathLengths.Add((from, to), dist);
            }

            var tEnd = previous.T - dist - 1;
            return (tEnd, previous.Score + (tEnd * valves[to].Flow));
        }

        var highScore = 0;
        var twelve = valves.Values.Where(valve => valve.Flow > 0).Select(valve => valve.Node).ToArray();
        var start = valves.Values.Single(valve => valve.Name == "AA").Node;

        void Search(uint from, uint[] too, (int T, int Score) previous, bool elephant)
        {
            for (var i = 0; i < too.Length; i++)
            {
                var to = too[i];
                var move = Move(from, to, previous);
                if (move.T >= 0)
                {
                    if (move.Score > highScore)
                    {
                        highScore = move.Score;
                    }

                    if (too.Length > 1)
                    {
                        Search(to, too.Where(j => j != to).ToArray(), move, elephant);
                    }
                }
                else if (!elephant && previous.Score >= highScore / 2)
                {
                    Search(start, too, (26, previous.Score), true);
                }
            }
        }

        // Part 1
        Search(start, twelve, (30, 0), true);

        Console.WriteLine(highScore);

        // Part 2
        Search(start, twelve, (26, 0), false);

        Console.WriteLine(highScore);
    }
}
