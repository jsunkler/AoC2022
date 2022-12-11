namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Part 1
            Game g1 = new Game();
            
            for (int i = 0; i< 20; i++)
            {
                g1.DoRound();
            }

            var a1 = g1.Monkeys.OrderByDescending(m => m.Inspections).Take(2).ToArray();
            Console.WriteLine($"Teil 1: {a1[0].Inspections * a1[1].Inspections}");

        }
    }
}