using System.Reflection;

string assemblyPath = Assembly.GetExecutingAssembly().Location;
string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
string textPath = Path.Combine(assemblyDirectory, "input.txt");

string[] input = File.ReadAllLines(textPath);

const string lowercase = "abcdefghijklmnopqrstuvwxyz";
string uppercase = lowercase.ToUpper();

// Part 1

int sum = 0;

foreach (string line in input)
{
    int len = line.Length / 2;

    char[] firstPart = line.Substring(0, len).ToCharArray();
    char[] secondPart = line.Substring(len).ToCharArray();

    foreach (var c in firstPart)
    {
        if (secondPart.Contains(c))
        {
            if (lowercase.Contains(c))
            {
                sum += lowercase.IndexOf(c) + 1;
            }
            else
            {
                sum += uppercase.IndexOf(c) + 27;
            }
            break;
        }
    }

}

Console.WriteLine($"Teil 1: {sum}");

// Part 2

sum = 0;

for (int i=0; i<input.Length; i+=3)
{
    char[] elv1 = input[i].ToCharArray();
    char[] elv2 = input[i + 1].ToCharArray();
    char[] elv3 = input[i + 2].ToCharArray();

    foreach (var c in elv1)
    {
        if (elv2.Contains(c) && elv3.Contains(c))
        {
            if (lowercase.Contains(c))
            {
                sum += lowercase.IndexOf(c) + 1;
            }
            else
            {
                sum += uppercase.IndexOf(c) + 27;
            }
            break;
        }
    }

}

Console.WriteLine($"Teil 2: {sum}");