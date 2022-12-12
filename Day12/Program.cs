using System.Reflection;

namespace Day12
{
    internal class Program
    {
        static readonly Dictionary<Coordinate2D, int> Map = new();
        static Coordinate2D start;
        static Coordinate2D end;

        static void Main(string[] args)
        {
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
#if DEBUG
            string textPath = Path.Combine(assemblyDirectory, "input.debug.txt");
#else
            string textPath = Path.Combine(assemblyDirectory, "input.txt");
#endif

            string[] input = File.ReadAllLines(textPath);

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x] == 'S')
                    {
                        start = (x, y);
                        Map[start] = 1;
                    }
                    else if (input[y][x] == 'E')
                    {
                        end = (x, y);
                        Map[end] = 26;
                    }
                    else
                    {
                        Map[(x, y)] = input[y][x] - 96;
                    }
                }
            }

            // Part 1

            Console.WriteLine($"Teil 1: {AStar(Map, start, end).Count - 1}");

            // Part 2

            List<int> lengths = new List<int>();

            List<KeyValuePair<Coordinate2D, int>> low = Map.Where(p => p.Value == 1).ToList();

            foreach (var kvp in low)
            {
                var path = AStar(Map, kvp.Key, end);
                if (path[0] == kvp.Key) lengths.Add(path.Count - 1);
            }

            Console.WriteLine($"Teil 2: {lengths.Min()}");
        }

        static List<Coordinate2D> AStar(Dictionary<Coordinate2D, int> Map, Coordinate2D start, Coordinate2D end)
        {
            Dictionary<Coordinate2D, Coordinate2D> cameFrom = new();
            PriorityQueue<Coordinate2D, int> openSet = new();
            Dictionary<Coordinate2D, int> gScore = new();
            Dictionary<Coordinate2D, int> fScore = new();
            HashSet<Coordinate2D> closedSet = new();

            gScore[start] = 0;
            fScore[start] = start.ManDistance(end);
            openSet.Enqueue(start, 0);

            while (openSet.Count > 0)
            {
                Coordinate2D current = openSet.Dequeue();
                if (!closedSet.Add(current)) continue;
                if (current == end) break;
                foreach (var n in current.Neighbors())
                {
                    if (Map.TryGetValue(n, out int height) && height <= Map[current] + 1)
                    {
                        var tGscore = gScore[current] + 1;
                        if (tGscore < gScore.GetValueOrDefault(n, int.MaxValue))
                        {
                            cameFrom[n] = current;
                            gScore[n] = tGscore;
                            fScore[n] = tGscore + n.ManDistance(end);
                            openSet.Enqueue(n, tGscore + n.ManDistance(end));
                        }
                    }
                }
            }

            return ReconstructPath(cameFrom, end);
        }

        static List<Coordinate2D> ReconstructPath(Dictionary<Coordinate2D, Coordinate2D> cameFrom, Coordinate2D current)
        {
            List<Coordinate2D> path = new() { current };
            while (cameFrom.TryGetValue(current, out var next))
            {
                path.Add(next);
                current = next;
            }
            path.Reverse();
            return path;
        }
    }
}