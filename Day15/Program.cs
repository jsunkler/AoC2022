using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Day15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Preparation for speed
            var process = Process.GetCurrentProcess();
            process.PriorityBoostEnabled = true;

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
                Sensor temp = new((sx, sy), (bx, by));

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

            Parallel.For(minX, maxX + 1, (x, state) =>
            {
                bool covered = false;

                if (sensors.Where(s => s.SensorPosition == (x, Y) || s.BeaconPosition == (x, Y)).Any()) return;

                foreach (Sensor sensor in sensors)
                {
                    if (sensor.CoversPoint((x, Y)))
                    {
                        covered = true;
                        break;
                    }
                }

                if (covered) Interlocked.Increment(ref counter);
            });

            Console.WriteLine($"Teil 1: {counter}");

            // Part 2

#if DEBUG
            int searchSpace = 20;
#else
            int searchSpace = 4_000_000;
#endif
            var area = GetUnseen(sensors.ToArray(), new Rectangle(0, 0, 4_000_001, 4_000_001)).First();
            Console.WriteLine($"Teil 2: {area.X * 4_000_000L + area.Y}");

        }

        static IEnumerable<Rectangle> GetUnseen(Sensor[] sensors, Rectangle rectangle)
        {
            // empty rectangle -> doesn't have uncovered areas 👍
            if (rectangle.Width == 0 || rectangle.Height == 0)
            {
                yield break;
            }

            // if all 4 corners of the rectangle are in range of one of the sensors -> it's covered 👍
            foreach (var sensor in sensors)
            {
                if (rectangle.Corners.All(corner => sensor.CoversPoint(corner)))
                {
                    yield break;
                }
            }

            // if the rectangle is 1x1 -> we just proved that it's uncovered 👍
            if (rectangle.Width == 1 && rectangle.Height == 1)
            {
                yield return rectangle;
                yield break;
            }

            // otherwise split the rectangle into smaller parts and recurse
            foreach (var rectangleT in rectangle.Split())
            {
                foreach (var unseenRect in GetUnseen(sensors, rectangleT))
                {
                    yield return unseenRect;
                }
            }
        }
    }
}