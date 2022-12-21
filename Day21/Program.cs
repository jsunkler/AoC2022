using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

// Preparation for speed
var process = Process.GetCurrentProcess();
process.PriorityClass = ProcessPriorityClass.High;
process.PriorityBoostEnabled = true;

string assemblyPath = Assembly.GetExecutingAssembly().Location;
string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
#if DEBUG
string textPath = Path.Combine(assemblyDirectory, "input.debug.txt");
#else
string textPath = Path.Combine(assemblyDirectory, "input.txt");
#endif

string[] input = File.ReadAllLines(textPath);

Regex regex1 = new Regex(@"^([a-z]{4}): (.*)$", RegexOptions.Compiled);
Regex regex2 = new Regex(@"([a-z]{4}) ([-*/+]) ([a-z]{4})", RegexOptions.Compiled);

Dictionary<string, Func<long>> monkeys = new();

foreach (string line in input)
{
    var match1 = regex1.Match(line);
    var match2 = regex2.Match(match1.Groups[2].Value);

    Func<long> retFunc = null;
    string monkeyId = match1.Groups[1].Value;
    if (!match2.Success)
    {
        long ret = long.Parse(match1.Groups[2].Value);
        retFunc = () => { return ret; };

        monkeys[monkeyId] = retFunc;
        continue;
    }

    string monkeyLeftId = match2.Groups[1].Value;
    string monkeyRightId = match2.Groups[3].Value;
    string op = match2.Groups[2].Value;

    switch (op)
    {
        case "+":
            retFunc = () => { return monkeys[monkeyLeftId].Invoke() + monkeys[monkeyRightId].Invoke(); };
            break;
        case "-":
            retFunc = () => { return monkeys[monkeyLeftId].Invoke() - monkeys[monkeyRightId].Invoke(); };
            break;
        case "*":
            retFunc = () => { return monkeys[monkeyLeftId].Invoke() * monkeys[monkeyRightId].Invoke(); };
            break;
        case "/":
            retFunc = () => { return monkeys[monkeyLeftId].Invoke() / monkeys[monkeyRightId].Invoke(); };
            break;
    }

    monkeys[monkeyId] = retFunc;
}

// Part 1
Stopwatch sw = new Stopwatch();
sw.Reset();
sw.Start();
long result = monkeys["root"].Invoke();
sw.Stop();
Console.WriteLine($"Part 1: {result} in {sw.Elapsed.TotalNanoseconds} ns");
