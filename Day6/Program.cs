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
char[] chars = input.ToCharArray();

// Part 1

for (int counter = 4; counter <= chars.Length; counter++ )
{
    var slice = chars[(counter - 4)..(counter)];
    if (slice.Distinct().Count() == 4)
    {
        Console.WriteLine($"Teil 1: {counter}");
        break;
    }
}

// Part 2
for (int counter = 14; counter <= chars.Length; counter++)
{
    var slice = chars[(counter - 14)..(counter)];
    if (slice.Distinct().Count() == 14)
    {
        Console.WriteLine($"Teil 2: {counter}");
        break;
    }
}