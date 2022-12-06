using System.Reflection;
using System.Text.RegularExpressions;

string assemblyPath = Assembly.GetExecutingAssembly().Location;
string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
#if DEBUG
string textPath = Path.Combine(assemblyDirectory, "input.debug.txt");
#else
string textPath = Path.Combine(assemblyDirectory, "input.txt");
#endif

string[] input = File.ReadAllLines(textPath);


#region Part1


/*
 * A - Rock
 * B - Paper
 * C - Scissors
 * 
 * X - Rock
 * Y - Paper
 * Z - Scissors
 * 
 */

Dictionary<string, int> values = new Dictionary<string, int>()
{
    { "A", 1 },
    { "B", 2 },
    { "C", 3 },
    { "X", 1 },
    { "Y", 2 },
    { "Z", 3 },
};

int score = 0;


foreach (string line in input)
{
    var match = Regex.Match(line, @"^(.) (.)$");
    var he = match.Groups[1].Value;
    var me = match.Groups[2].Value;
    
    if (values[me] == values[he])
    {
        score += values[me] + 3;
        continue;
    }
    if (
        (values[me] == 1 && values[he] == 3) ||
        (values[me] == 2 && values[he] == 1) ||
        (values[me] == 3 && values[he] == 2)
        )
    {
        score += values[me] + 6;
        continue;
    }
    score += values[me];
}

Console.WriteLine($"Teil 1: {score}");

#endregion

#region Part2

score = 0;

foreach (string line in input)
{
    var match = Regex.Match(line, @"^(.) (.)$");
    var he = match.Groups[1].Value;
    var outcome = match.Groups[2].Value;
    var me = "";

    if (outcome == "X") //loose
    {
        if (he == "A") me = "C";
        if (he == "B") me = "A";
        if (he == "C") me = "B";
    }
    if (outcome == "Y") //draw
    {
        me = he;
    }
    if (outcome == "Z") //win
    {
        if (he == "A") me = "B";
        if (he == "B") me = "C";
        if (he == "C") me = "A";
    }


    if (values[me] == values[he])
    {
        score += values[me] + 3;
        continue;
    }
    if (
        (values[me] == 1 && values[he] == 3) ||
        (values[me] == 2 && values[he] == 1) ||
        (values[me] == 3 && values[he] == 2)
        )
    {
        score += values[me] + 6;
        continue;
    }
    score += values[me];
}

Console.WriteLine($"Teil 2: {score}");

#endregion