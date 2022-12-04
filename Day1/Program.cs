using System.Reflection;
using System.Text.RegularExpressions;

string assemblyPath = Assembly.GetExecutingAssembly().Location;
string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
string textPath = Path.Combine(assemblyDirectory, "input.txt");

string[] input = File.ReadAllLines(textPath);

List<int> calories = new List<int>();

int sum = 0;

foreach (string line in input)
{
    if (Regex.IsMatch(line, @"^\d+$"))
    {
        sum += int.Parse(line);
    }
    else
    {
        calories.Add(sum);
        sum = 0;
    }
}

Console.WriteLine($"Teil 1: {calories.Max()}");
Console.WriteLine($"Teil 2: {calories.OrderDescending().Take(3).Sum()}");