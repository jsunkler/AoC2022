using Day17;
using System.Diagnostics.Metrics;
using System.Reflection;

string assemblyPath = Assembly.GetExecutingAssembly().Location;
string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
#if DEBUG
string textPath = Path.Combine(assemblyDirectory, "input.debug.txt");
#else
string textPath = Path.Combine(assemblyDirectory, "input.txt");
#endif

string input = File.ReadAllText(textPath);

CircularList<(int xOffset, int yOffset)> movements = new();

movements.AddRange(input.ToCharArray().Select(c => c == '<' ? (-1, 0) : (1, 0)));

// Part 1
Game g1 = new Game(movements);
g1.Play();

Console.WriteLine($"Teil 1: {g1.GetHeight()}");

Game g2 = new Game(movements);
g2.Play(1_000_000_000_000);

Console.WriteLine($"Teil 2: {g2.GetHeight()}");