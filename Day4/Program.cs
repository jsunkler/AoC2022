using Day4;
using System.Reflection;
using System.Text.RegularExpressions;

string assemblyPath = Assembly.GetExecutingAssembly().Location;
string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
string textPath = Path.Combine(assemblyDirectory, "input.txt");

string[] input = File.ReadAllLines(textPath);

// Part 1

int counter = 0;

foreach (string line in input)
{
    var match = Regex.Match(line, @"^(\d+)-(\d+),(\d+)-(\d+)$");
    int firstLower = int.Parse(match.Groups[1].Value);
    int firstUpper = int.Parse(match.Groups[2].Value);
    int secondLower = int.Parse(match.Groups[3].Value);
    int secondUpper = int.Parse(match.Groups[4].Value);

    CustomRange r1 = new CustomRange(firstLower, firstUpper);
    CustomRange r2 = new CustomRange(secondLower, secondUpper);

    if (r1.Includes(r2) || r2.Includes(r1)) counter++;
}

Console.WriteLine($"Teil 1: {counter}");

// Part 2

counter = 0;

foreach (string line in input)
{
    var match = Regex.Match(line, @"^(\d+)-(\d+),(\d+)-(\d+)$");
    int firstLower = int.Parse(match.Groups[1].Value);
    int firstUpper = int.Parse(match.Groups[2].Value);
    int secondLower = int.Parse(match.Groups[3].Value);
    int secondUpper = int.Parse(match.Groups[4].Value);

    List<int> first = new List<int>();
    List<int> second = new List<int>();

    first.AddRange(Enumerable.Range(firstLower, firstUpper-firstLower+1));
    second.AddRange(Enumerable.Range(secondLower, secondUpper-secondLower+1));

    if (first.Intersect(second).Any()) counter++;
}

Console.WriteLine($"Teil 2: {counter}");