﻿using System.Diagnostics.Metrics;
using System.Reflection;

string assemblyPath = Assembly.GetExecutingAssembly().Location;
string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
#if DEBUG
string textPath = Path.Combine(assemblyDirectory, "input.debug.txt");
#else
string textPath = Path.Combine(assemblyDirectory, "input.txt");
#endif

string[] input = File.ReadAllLines(textPath);

// parse input

int xMax = input[0].Length;
int yMax = input.Length;

byte[,] treeMap = new byte[xMax, yMax];

// construct map from input
for (int x = 0; x < xMax; x++)
{
    for (int y = 0; y < yMax; y++)
    {
        treeMap[x, y] = byte.Parse(input[y][x].ToString());
    }
}

// part 1

// saving visible trees in a set of ValueTuples of coordinates
HashSet<(int, int)> visibleTrees = new HashSet<(int, int)> ();

// for every line
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

// for every column
for (int x = 0; x < xMax; x++)
{
    short lastVal = -1;
    // from top to bottom
    for (int y = 0; y < yMax; y++)
    {
        if (treeMap[x, y] > lastVal)
        {
            lastVal = treeMap[x, y];
            visibleTrees.Add((x, y));
        }
    }

    lastVal = -1;
    // from bottom to top
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

// part 2

// creating a score map for holding scores
int[,] scoreMap = new int[xMax, yMax];

// trees on edges will always have a score of 0
for (int x = 1; x < xMax-1; x++)
{
    for (int y = 1; y < yMax-1; y++)
    {
        byte currVal = treeMap[x, y];

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

Console.WriteLine($"Teil 2: {scoreMap.Cast<int>().Max()}");