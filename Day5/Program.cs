using System.Diagnostics.Metrics;
using System.Reflection;
using System.Text.RegularExpressions;

// Other way to Parse directly: (\[\w\]|   )\s?

string assemblyPath = Assembly.GetExecutingAssembly().Location;
string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
#if DEBUG
string textPath = Path.Combine(assemblyDirectory, "input.debug.txt");
#else
string textPath = Path.Combine(assemblyDirectory, "input.txt");
#endif

string[] input = File.ReadAllLines(textPath);

// Part 1

Stack<char>[] stacks = GetStacks();

foreach (var line in input)
{
    var match = Regex.Match(line, @"^move (\d+) from (\d) to (\d)$");
    int num = int.Parse(match.Groups[1].Value);
    int from = int.Parse(match.Groups[2].Value) - 1;
    int to = int.Parse(match.Groups[3].Value) - 1;

    for (int i = 0; i < num; i++)
        stacks[to].Push(stacks[from].Pop());
}

String output = String.Empty;

for (int i = 0; i < stacks.Length; i++)
{
    output += stacks[i].Peek();
}

Console.WriteLine($"Teil 1: {output}");

// Part 2

stacks = GetStacks();

foreach (var line in input)
{
    var match = Regex.Match(line, @"^move (\d+) from (\d) to (\d)$");
    int num = int.Parse(match.Groups[1].Value);
    int from = int.Parse(match.Groups[2].Value) - 1;
    int to = int.Parse(match.Groups[3].Value) - 1;

    Stack<char> temp = new Stack<char>();

    for (int i = 0; i < num; i++)
        temp.Push(stacks[from].Pop());

    for (int i = 0; i < num; i++)
        stacks[to].Push(temp.Pop());

}

output = String.Empty;

for (int i = 0; i < stacks.Length; i++)
{
    output += stacks[i].Peek();
}

Console.WriteLine($"Teil 2: {output}");

static Stack<char>[] GetStacks()
{
#if DEBUG
    Stack<char>[] stacks = new Stack<char>[3];
    stacks[0] = new Stack<char>();
    stacks[0].Push('Z');
    stacks[0].Push('N');
    stacks[1] = new Stack<char>();
    stacks[1].Push('M');
    stacks[1].Push('C');
    stacks[1].Push('D');
    stacks[2] = new Stack<char>();
    stacks[2].Push('P');
#else
            Stack<char>[] stacks = new Stack<char>[9];
            stacks[0] = new Stack<char>();
            stacks[0].Push('S');
            stacks[0].Push('Z');
            stacks[0].Push('P');
            stacks[0].Push('D');
            stacks[0].Push('L');
            stacks[0].Push('B');
            stacks[0].Push('F');
            stacks[0].Push('C');
            stacks[1] = new Stack<char>();
            stacks[1].Push('N');
            stacks[1].Push('V');
            stacks[1].Push('G');
            stacks[1].Push('P');
            stacks[1].Push('H');
            stacks[1].Push('W');
            stacks[1].Push('B');
            stacks[2] = new Stack<char>();
            stacks[2].Push('F');
            stacks[2].Push('W');
            stacks[2].Push('B');
            stacks[2].Push('J');
            stacks[2].Push('G');
            stacks[3] = new Stack<char>();
            stacks[3].Push('G');
            stacks[3].Push('J');
            stacks[3].Push('N');
            stacks[3].Push('F');
            stacks[3].Push('L');
            stacks[3].Push('W');
            stacks[3].Push('C');
            stacks[3].Push('S');
            stacks[4] = new Stack<char>();
            stacks[4].Push('W');
            stacks[4].Push('J');
            stacks[4].Push('L');
            stacks[4].Push('T');
            stacks[4].Push('P');
            stacks[4].Push('M');
            stacks[4].Push('S');
            stacks[4].Push('H');
            stacks[5] = new Stack<char>();
            stacks[5].Push('B');
            stacks[5].Push('C');
            stacks[5].Push('W');
            stacks[5].Push('G');
            stacks[5].Push('F');
            stacks[5].Push('S');
            stacks[6] = new Stack<char>();
            stacks[6].Push('H');
            stacks[6].Push('T');
            stacks[6].Push('P');
            stacks[6].Push('M');
            stacks[6].Push('Q');
            stacks[6].Push('B');
            stacks[6].Push('W');
            stacks[7] = new Stack<char>();
            stacks[7].Push('F');
            stacks[7].Push('S');
            stacks[7].Push('W');
            stacks[7].Push('T');
            stacks[8] = new Stack<char>();
            stacks[8].Push('N');
            stacks[8].Push('C');
            stacks[8].Push('R');
#endif

    return stacks;
}