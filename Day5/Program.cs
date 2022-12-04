using System.Diagnostics.Metrics;
using System.Reflection;

string assemblyPath = Assembly.GetExecutingAssembly().Location;
string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
string textPath = Path.Combine(assemblyDirectory, "input.txt");

string[] input = File.ReadAllLines(textPath);

// Part 1

int counter = 0;

foreach (string line in input)
{
}

Console.WriteLine($"Teil 1: {counter}");