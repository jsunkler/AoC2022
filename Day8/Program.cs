using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reflection;

string assemblyPath = Assembly.GetExecutingAssembly().Location;
string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
#if DEBUG
string textPath = Path.Combine(assemblyDirectory, "input.debug.txt");
#else
string textPath = Path.Combine(assemblyDirectory, "input.txt");
#endif

string[] input = File.ReadAllLines(textPath);

// Preparation for speed
var process = Process.GetCurrentProcess();
process.PriorityClass = ProcessPriorityClass.High;
process.PriorityBoostEnabled = true;

// parse input

Stopwatch stopwatch = Stopwatch.StartNew();

int xMax = input[0].Length;
int yMax = input.Length;

sbyte[,] treeMap = new sbyte[xMax, yMax];

// construct map from input
for (int x = 0; x < xMax; x++)
{
    for (int y = 0; y < yMax; y++)
    {
        treeMap[x, y] = sbyte.Parse(input[y][x].ToString());
    }
}

stopwatch.Stop();
Console.WriteLine($"Input preparation took {stopwatch.Elapsed.TotalMilliseconds:F4} ms and {stopwatch.ElapsedTicks} ticks");

// part 1

stopwatch.Restart();

// saving visible trees in a set of ValueTuples of coordinates
bool[,] visibilityMap = new bool[xMax, yMax];

// for every line
for (int y = 0; y < yMax; y++)
{
    sbyte lastValL = -1;
    sbyte lastValR = -1;

    for (int x = 0; x < xMax; x++)
    {
        if (treeMap[x, y] > lastValL) 
        { 
            lastValL = treeMap[x, y];
            visibilityMap[x, y] = true;
        }
        if (treeMap[xMax - x - 1, y] > lastValR)
        {
            lastValR = treeMap[xMax - x - 1, y];
            visibilityMap[xMax - x - 1, y] = true;
        }
    }
}

// for every column
for (int x = 0; x < xMax; x++)
{
    sbyte lastValT = -1;
    sbyte lastValB = -1;

    for (int y = 0; y < yMax; y++)
    {
        if (treeMap[x, y] > lastValT)
        {
            lastValT = treeMap[x, y];
            visibilityMap[x, y] = true;
        }
        if (treeMap[x, yMax - y -1] > lastValB)
        {
            lastValB = treeMap[x, yMax - y - 1];
            visibilityMap[x, yMax - y - 1] = true;
        }
    }

}

stopwatch.Stop();
Console.WriteLine($"Part 1 took {stopwatch.Elapsed.TotalMilliseconds:F4} ms and {stopwatch.ElapsedTicks} ticks");
Console.WriteLine($"Part 1: {visibilityMap.Cast<bool>().Where(v => v).Count()}");

// part 2

stopwatch.Restart();

// creating a score map for holding scores
int[,] scoreMap = new int[xMax, yMax];

// trees on edges will always have a score of 0
for (int x = 1; x < xMax-1; x++)
{
    for (int y = 1; y < yMax-1; y++)
    {
        sbyte currVal = treeMap[x, y];

        int scoreLeft = 0;
        int xOffset = -1;
        // iterating to the left
        while (x+xOffset >= 0)
        {
            scoreLeft++;
            if (treeMap[x + xOffset, y] >= currVal) break;
            xOffset--;
        }

        int scoreRight = 0;
        xOffset = 1;
        // iterating to the right
        while (x + xOffset < xMax)
        {
            scoreRight++;
            if (treeMap[x + xOffset, y] >= currVal) break;
            xOffset++;
        }

        int scoreUp = 0;
        int yOffset = -1;
        // iterating to the top
        while (y + yOffset >= 0)
        {
            scoreUp++;
            if (treeMap[x, y + yOffset] >= currVal) break;
            yOffset--;
        }

        int scoreDown = 0;
        yOffset = 1;
        // iterating to the bottom
        while (y + yOffset < yMax)
        {
            scoreDown++;
            if (treeMap[x, y + yOffset] >= currVal) break;
            yOffset++;
        }

        // saving score
        scoreMap[x, y] = scoreLeft * scoreRight * scoreUp * scoreDown;
    }
}
stopwatch.Stop();
Console.WriteLine($"Part 2 took {stopwatch.Elapsed.TotalMilliseconds:F4} ms and {stopwatch.ElapsedTicks} ticks");
Console.WriteLine($"Part 2: {scoreMap.Cast<int>().Max()}");