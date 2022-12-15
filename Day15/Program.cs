using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Day15
{
    internal class Program
    {
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

            int counter = 0;

            Regex regex = new Regex(@"^Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)$", RegexOptions.Compiled);
            List<Sensor> sensors = new List<Sensor>();

            int minX = 0;
            int minY = 0;
            int maxX = 0;
            int maxY = 0;

            foreach (string line in input)
            {
                var match = regex.Match(line);
                int sx = int.Parse(match.Groups[1].Value);
                int sy = int.Parse(match.Groups[2].Value);
                int bx = int.Parse(match.Groups[3].Value);
                int by = int.Parse(match.Groups[4].Value);
                Sensor temp = new Sensor((sx, sy), (bx, by));

                minX = Math.Min(minX, sx - temp.CoveredDistance);
                minY = Math.Min(minY, sy - temp.CoveredDistance);
                maxX = Math.Max(maxX, sx + temp.CoveredDistance);
                maxY = Math.Max(maxY, sy + temp.CoveredDistance);

                sensors.Add(temp);
            }

            // Part 1

#if DEBUG
            int Y = 10;
#else
            int Y = 2_000_000;
#endif

            for (int x = minX; x <= maxX; x++)
            {
                bool covered = false;

                if (sensors.Where(s => s.SensorPosition == (x, Y) || s.BeaconPosition == (x, Y)).Any()) continue;

                foreach (Sensor sensor in sensors) 
                { 
                    if (sensor.CoversPoint((x, Y)))
                    {
                        covered = true;
                        break;
                    }
                }

                if (covered) counter++;
            }

            Console.WriteLine($"Teil 1: {counter}");

            // Part 2

#if DEBUG
            int searchSpace = 20;
#else
            int searchSpace = 4_000_000;
#endif

            for (int x = 0; x < searchSpace; x++)
            {
                for (int y = 0; y < searchSpace; y++)
                {
                    bool covered = false;

                    if (sensors.Where(s => s.SensorPosition == (x, y) || s.BeaconPosition == (x, y)).Any()) continue;

                    foreach (Sensor sensor in sensors)
                    {
                        if (sensor.CoversPoint((x, y)))
                        {
                            covered = true;
                            break;
                        }
                    }

                    if (!covered)
                    {
                        Console.WriteLine($"Teil 2: {x * 4_000_000 + y}");
                    }
                }
            }
        }
    }
}