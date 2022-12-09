using System.Diagnostics.Metrics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

string assemblyPath = Assembly.GetExecutingAssembly().Location;
string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
#if DEBUG
string textPath = Path.Combine(assemblyDirectory, "input.debug.txt");
#else
string textPath = Path.Combine(assemblyDirectory, "input.txt");
#endif

string[] input = File.ReadAllLines(textPath);

// Part 1

HashSet<(int, int)> visitedPoints = new HashSet<(int, int)> ();
visitedPoints.Add((1, 1));

int headPosX = 1, headPosY = 1;
int tailPosX = 1, tailPosY = 1;

foreach (string line in input)
{
    var match = Regex.Match(line, @"^(\w) (\d+)$");
    string action = match.Groups[1].Value;
    int value = int.Parse(match.Groups[2].Value);

    for (int i = 0; i < value; i++)
    {
        if (action == "R") headPosX++;
        if (action == "L") headPosX--;
        if (action == "U") headPosY++;
        if (action == "D") headPosY--;

        var newPos = getTailPos((headPosX, headPosY), (tailPosX, tailPosY));
        visitedPoints.Add(newPos);
        tailPosX = newPos.x; 
        tailPosY = newPos.y;
    }
}

Console.WriteLine($"Teil 1: {visitedPoints.Count}");

// Part 2
visitedPoints = new HashSet<(int, int)>();
(int x, int y)[] positions = new (int x, int y)[10]
{
    (1, 1),(1, 1),(1, 1),(1, 1),(1, 1),(1, 1),(1, 1),(1, 1),(1, 1),(1, 1)
};

foreach (string line in input)
{
    var match = Regex.Match(line, @"^(\w) (\d+)$");
    string action = match.Groups[1].Value;
    int value = int.Parse(match.Groups[2].Value);

    for (int i = 0; i < value; i++)
    {
        if (action == "R") positions[0].x++;
        if (action == "L") positions[0].x--;
        if (action == "U") positions[0].y++;
        if (action == "D") positions[0].y--;

        for (int j = 1; j < positions.Length; j++)
        {
            var newPos = getTailPos((positions[j-1].x, positions[j - 1].y), (positions[j].x, positions[j].y));
            positions[j] = newPos;
        }
        visitedPoints.Add(positions[^1]);
    }
}

Console.WriteLine($"Teil 2: {visitedPoints.Count}");

static (int x, int y) getTailPos((int x, int y) headPos, (int x, int y) tailPos)
{
    // same column
    if (headPos.x == tailPos.x)
    {
        if (headPos.y - tailPos.y > 1) 
        {
            return (tailPos.x, tailPos.y + 1);
        }
        if (headPos.y - tailPos.y < -1)
        {
            return (tailPos.x, tailPos.y - 1);
        }
    }

    //same row
    if (headPos.y == tailPos.y)
    {
        if (headPos.x - tailPos.x > 1)
        {
            return (tailPos.x + 1, tailPos.y);
        }
        if (headPos.x - tailPos.x < -1)
        {
            return (tailPos.x - 1, tailPos.y);
        }
    }

    // diagonal
    if (headPos.x > tailPos.x && 
        headPos.y > tailPos.y &&
        (Math.Abs(headPos.x - tailPos.x) > 1 || Math.Abs(headPos.y - tailPos.y) > 1))
        return (tailPos.x + 1, tailPos.y + 1);
    if (headPos.x > tailPos.x && 
        headPos.y < tailPos.y &&
        (Math.Abs(headPos.x - tailPos.x) > 1 || Math.Abs(headPos.y - tailPos.y) > 1)) 
        return (tailPos.x + 1, tailPos.y - 1);
    if (headPos.x < tailPos.x && 
        headPos.y > tailPos.y &&
        (Math.Abs(headPos.x - tailPos.x) > 1 || Math.Abs(headPos.y - tailPos.y) > 1)) 
        return (tailPos.x - 1, tailPos.y + 1);
    if (headPos.x < tailPos.x && 
        headPos.y < tailPos.y &&
        (Math.Abs(headPos.x - tailPos.x) > 1 || Math.Abs(headPos.y - tailPos.y) > 1)) 
        return (tailPos.x - 1, tailPos.y - 1);

    return tailPos;
}

