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

// Parse input

int xMax = input[0].Length;
int yMax = input.Length;

byte[,] treeMap = new byte[xMax, yMax];

for (int x = 0; x < xMax; x++)
{
    for (int y = 0; y < yMax; y++)
    {
        treeMap[x, y] = byte.Parse(input[y][x].ToString());
    }
}

// Part 1

HashSet<(int, int)> visibleTrees = new HashSet<(int, int)> ();

// For every line
for (int y = 0; y < yMax; y++)
{
    short lastVal = -1;
    // from left to right
    for (int x = 0; x < xMax; x++)
    {
        if (treeMap[x, y] > lastVal) 
        { 
            lastVal = treeMap[x, y];
            visibleTrees.Add((x, y));
        }
    }

    lastVal = -1;
    // from right to left
    for (int x = xMax - 1; x >= 0; x--)
    {
        if (treeMap[x, y] > lastVal)
        {
            lastVal = treeMap[x, y];
            visibleTrees.Add((x, y));
        }
    }
}

for (int x = 0; x < xMax; x++)
{
    short lastVal = -1;
    // from left to right
    for (int y = 0; y < yMax; y++)
    {
        if (treeMap[x, y] > lastVal)
        {
            lastVal = treeMap[x, y];
            visibleTrees.Add((x, y));
        }
    }

    lastVal = -1;
    // from right to left
    for (int y = yMax - 1; y >= 0; y--)
    {
        if (treeMap[x, y] > lastVal)
        {
            lastVal = treeMap[x, y];
            visibleTrees.Add((x, y));
        }
    }
}

Console.WriteLine($"Teil 1: {visibleTrees.Count}");

// Part 2

int[,] scoreMap = new int[xMax, yMax];

for (int x = 1; x < xMax-1; x++)
{
    for (int y = 1; y < yMax-1; y++)
    {
        byte currVal = treeMap[x, y];

        int scoreLeft = 0;
        int xOffset = -1;
        while (x+xOffset >= 0)
        {
            scoreLeft++;
            if (treeMap[x + xOffset, y] >= currVal) break;
            xOffset--;
        }

        int scoreRight = 0;
        xOffset = 1;
        while (x + xOffset < xMax)
        {
            scoreRight++;
            if (treeMap[x + xOffset, y] >= currVal) break;
            xOffset++;
        }

        int scoreUp = 0;
        int yOffset = -1;
        while (y + yOffset >= 0)
        {
            scoreUp++;
            if (treeMap[x, y + yOffset] >= currVal) break;
            yOffset--;
        }

        int scoreDown = 0;
        yOffset = 1;
        while (y + yOffset < yMax)
        {
            scoreDown++;
            if (treeMap[x, y + yOffset] >= currVal) break;
            yOffset++;
        }

        scoreMap[x, y] = scoreLeft * scoreRight * scoreUp * scoreDown;
    }
}

Console.WriteLine($"Teil 2: {scoreMap.Cast<int>().Max()}");