using System.Diagnostics.Metrics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

string assemblyPath = Assembly.GetExecutingAssembly().Location;
string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
#if DEBUG
string textPath = Path.Combine(assemblyDirectory, "input.debug.txt");
#else
string textPath = Path.Combine(assemblyDirectory, "input.txt");
#endif

string[] input = File.ReadAllLines(textPath);

// Part 1

List<(int pre, int dur, int aft)> history = new List<(int pre, int dur, int aft)>();
history.Add((1, 1, 1));
Regex regex = new Regex(@"^(addx|noop)( (-?\d+))?$", RegexOptions.Compiled);

foreach (string line in input)
{
    var match = regex.Match(line);
    if (match.Groups[1].Value == "noop") {
        var last = history.Last();
        history.Add((last.aft, last.aft, last.aft));
    }
    if (match.Groups[1].Value == "addx")
    {
        int add = int.Parse(match.Groups[3].Value);
        var last = history.Last();
        history.Add((last.aft, last.aft, last.aft));
        history.Add((last.aft, last.aft, last.aft+add));
    }
}

int counter = 20 * history[20].dur +
    60 * history[60].dur +
    100 * history[100].dur +
    140 * history[140].dur +
    180 * history[180].dur +
    220 * history[220].dur;

Console.WriteLine($"Teil 1: {counter}");

// Part 2

char[,] output = new char[6,40];

// Fill array
for (int row = 0; row < 6; row++)
{
    for (int col = 0; col < 40; col++)
    {
        output[row, col] = '.';
    }
}

for (int i = 1; i < history.Count; i++)
{
    int currentDrawingPositionCol = (i-1) % 40;
    int currentDrawingPositionRow = (i-1) / 40;

    int spriteFromX = history[i].dur - 1;
    int spriteToX = spriteFromX + 2;

    if (currentDrawingPositionCol >= spriteFromX && currentDrawingPositionCol <= spriteToX)
    {
        output[currentDrawingPositionRow, currentDrawingPositionCol] = '#';
    }
}

printOutput(output);

static void printOutput(char[,] data)
{
    for (int row = 0; row < 6; row++)
    {
        for (int col = 0; col < 40; col++)
        {
            Console.Write(data[row, col]);
        }
        Console.WriteLine();
    }
}