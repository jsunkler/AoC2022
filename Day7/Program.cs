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

Node root = new DirectoryNode("/");
List<DirectoryNode> allDirs = new List<DirectoryNode>();
Node current = null;

foreach (string line in input)
{
    var match = Regex.Match(line, @"^\$ (cd|ls) ([\w./]+)$");
    if (match.Success)
    {
        // Command mode
        string cmd = match.Groups[1].Value;
        string arg = match.Groups[2].Value;

        if (cmd == "cd")
        {
            if (arg == "..")
            {
                if (current != null && current.Parent != null)
                {
                    current = current.Parent;
                }
            }
            else if (arg == "/")
            {
                current = root;
            }
            else
            {
                current = current.Children.Where(c => c.Name == arg).FirstOrDefault();
            }
        }

        continue;
    }

    var match2 = Regex.Match(line, @"^dir (\w+)$");
    if (match2.Success)
    {
        // Output dir
        string dirName = match2.Groups[1].Value;

        if (current != null)
        {
            var d = new DirectoryNode(dirName);
            current.AddChild(d);
            allDirs.Add(d);
        }

        continue;
    }

    var match3 = Regex.Match(line, @"^(\d+)\s(.+)$");
    if (match3.Success)
    {
        //Output file
        long size = long.Parse(match3.Groups[1].Value);
        string fileName = match3.Groups[2].Value;

        if (current != null)
        {
            var f = new FileNode(size, fileName);
            current.AddChild(f);
        }

        continue;
    }

}

long part1 = allDirs.Where(d => d.GetSize() <= 100000).Sum(d => d.GetSize());

Console.WriteLine($"Teil 1: {part1}");

// Part 2

long fullSize = root.GetSize();
long freeSpace = 70000000 - fullSize;
long spaceToReclaim = 30000000 - freeSpace;

long part2 = allDirs.Where(d => d.GetSize() >= spaceToReclaim).OrderBy(d => d.GetSize()).Select(d => d.GetSize()).FirstOrDefault();

Console.WriteLine($"Teil 2: {part2}");

abstract class Node
{
    public List<Node> Children { get; private set; } = new List<Node>();

    public Node? Parent { get; set; }

    public string Name { get; set; }

    public abstract long GetSize();

    public void AddChild(Node child)
    {
        Children.Add(child);
        child.Parent = this;
    }
}

class FileNode : Node
{
    public long Size { get; set; }

    public FileNode(long size, string name)
    {
        Size = size;
        Name = name;
    }

    public override long GetSize()
    {
        return Size;
    }
}

class DirectoryNode : Node
{
    

    public DirectoryNode(string name)
    {
        Name = name;
    }

    public override long GetSize()
    {
        return Children
            .Sum(c => c.GetSize());
    }
}