using System.Reflection;
using System.Linq;

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

        string[] input = File.ReadAllLines(textPath);

        // Part 1

        var caveMap = getCave(input);
        printCave(caveMap);

        int counter = fill(caveMap, (500, 0));
        printCave(caveMap);

        Console.WriteLine($"Teil 1: {counter}");

        // Part 2
        caveMap = getCave(input);
        printCave(caveMap);

        counter = fill(caveMap, (500, 0), true);
        printCave(caveMap);

        Console.WriteLine($"Teil 2: {counter}");

    }

    public static Dictionary<(int x, int y), char> getCave(string[] input)
    {
        Dictionary<(int x, int y), char> caveMap = new();

        foreach (string line in input)
        {
            (int x, int y)[] steps = (
                from step in line.Split(" -> ")
                let points = step.Split(",")
                select (int.Parse(points[0]), int.Parse(points[1]))
                ).ToArray();

            for (int i = 1; i < steps.Length; i++)
            {
                fill(caveMap, steps[i - 1], steps[i]);
            }
        }

        return caveMap;
    }

    private static int fill(Dictionary<(int x, int y), char> map, (int x, int y) sandSource, bool withFloor = false)
    {
        int maxY = (from kvp in map
                    select kvp.Key.y).Max();

        while (true)
        {
            (int x, int y) loc = simulate(map, sandSource, maxY);

            if (map.ContainsKey(loc)) break;

            // Void
            if (!withFloor && loc.y >= maxY + 1) break;

            map[loc] = 'o';
        }

        return map.Values.Count(x => x == 'o');
    }

    private static (int x, int y) simulate(Dictionary<(int x, int y), char> map, (int x, int y) sand, int maxY)
    {
        (int x, int y) offset1 = (0, 1);
        (int x, int y) offset2 = (-1, 1);
        (int x, int y) offset3 = (1, 1);

        while (sand.y < maxY + 1)
        {
            (int x, int y) sandOffset1 = (sand.x + offset1.x, sand.y + offset1.y);
            (int x, int y) sandOffset2 = (sand.x + offset2.x, sand.y + offset2.y);
            (int x, int y) sandOffset3 = (sand.x + offset3.x, sand.y + offset3.y);

            if (!map.ContainsKey(sandOffset1))
            {
                sand.x += offset1.x;
                sand.y += offset1.y;
            }
            else if (!map.ContainsKey(sandOffset2))
            {
                sand.x += offset2.x;
                sand.y += offset2.y;
            }
            else if (!map.ContainsKey(sandOffset3))
            {
                sand.x += offset3.x;
                sand.y += offset3.y;
            }
            else
            {
                break;
            }
        }

        return sand;
    }

    private static void printCave(Dictionary<(int x, int y), char> map)
    {
        int maxX = (from kvp in map
                    select kvp.Key.x).Max();
        int minX = (from kvp in map
                    select kvp.Key.x).Min();
        int maxY = (from kvp in map
                    select kvp.Key.y).Max();
        int minY = (from kvp in map
                    select kvp.Key.y).Min();

        Console.Clear();

        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                char o = map.GetValueOrDefault((x, y), '.');
                Console.Write(o);
            }
            Console.WriteLine();
        }
    }

    private static void fill(Dictionary<(int x, int y), char> map, (int x, int y) from, (int x, int y) to)
    {
        if (from.x == to.x) //vertical
        {
            int direction = Math.Sign(to.y - from.y);
            for (int y = from.y; y != to.y + direction; y += direction)
            {
                map[(from.x, y)] = '#';
            }
        }
        if (from.y == to.y) //horizontal
        {
            int direction = Math.Sign(to.x - from.x);
            for (int x = from.x; x != to.x + direction; x += direction)
            {
                map[(x, from.y)] = '#';
            }
        }
    }
}
