using System.Reflection;
using System.Text.Json.Nodes;

internal class Program
{
    private static void Main(string[] args)
    {
        string assemblyPath = Assembly.GetExecutingAssembly().Location;
        string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
#if DEBUG
        string textPath = Path.Combine(assemblyDirectory, "input.debug.txt");
#else
string textPath = Path.Combine(assemblyDirectory, "input.txt");
#endif

        string[] input = File.ReadAllLines(textPath);

        // Part 1

        var result = input
            .Where(line => !string.IsNullOrEmpty(line))
            .Select(line => JsonNode.Parse(line))
            .Chunk(2)
            .Select((pair, index) => Compare(pair[0], pair[1]) < 0 ? index + 1 : 0)
            .Sum();

        Console.WriteLine($"Teil 1: {result}");

        IEnumerable<JsonNode> packets = input
            .Where(line => !string.IsNullOrEmpty(line))
            .Select(line => JsonNode.Parse(line));

        List<JsonNode> packets2 = new[] { "[[2]]", "[[6]]" }
            .Select(line => JsonNode.Parse(line))
            .ToList();

        List<JsonNode> sorted = packets.Concat(packets2).ToList();
        sorted.Sort(Compare);

        Console.WriteLine($"Teil 2: {(sorted.IndexOf(packets2[0]) + 1) * (sorted.IndexOf(packets2[1]) + 1)}");
    }

    public static int Compare(JsonNode left, JsonNode right)
    {
        if (left is JsonValue && right is JsonValue) return (int)left - (int)right;

        JsonArray arrLeft = left as JsonArray ?? new JsonArray((int)left);
        JsonArray arrRight = right as JsonArray ?? new JsonArray((int)right);

        return Enumerable
            .Zip(arrLeft, arrRight)
            .Select(p => Compare(p.First, p.Second))
            .FirstOrDefault(c => c != 0, arrLeft.Count - arrRight.Count);
    }
}

#if DEBUG

#else
#endif
