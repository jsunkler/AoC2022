using System.Diagnostics;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Preparation for speed
            var process = Process.GetCurrentProcess();
            process.PriorityClass = ProcessPriorityClass.High;
            process.PriorityBoostEnabled = true;

            // Part 1
            Game g1 = new Game();
            
            for (int i = 0; i< 20; i++)
            {
                g1.DoRound();
            }

            var a1 = g1.Monkeys.OrderByDescending(m => m.Inspections).Take(2).ToArray();
            Console.WriteLine($"Teil 1: {a1[0].Inspections * a1[1].Inspections}");

            // Part 1
            Game g2 = new Game();

            for (int i = 0; i < 10000; i++)
            {
                g2.DoRound(true);
            }

            var a2 = g2.Monkeys.OrderByDescending(m => m.Inspections).Take(2).ToArray();
            Console.WriteLine($"Teil 2: {a2[0].Inspections * a2[1].Inspections}");
        }
    }
}