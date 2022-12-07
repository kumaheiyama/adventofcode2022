using System.Text.RegularExpressions;

internal partial class Advent7
{
    private static int score1 = 0;
    private static readonly List<int> deletes = new();
    private static int lack = 0;

    internal static async Task Run()
    {
        var fileContent = await File.ReadAllLinesAsync("input7.txt");

        var neededSpace = 40000000;
        var baseDir = new Dir("/");
        PopulateBaseDir(fileContent, baseDir);

        GetDirSize(baseDir);

        var totalDirSize = baseDir.GetSize();
        lack = totalDirSize - neededSpace;
        baseDir.GetSize();
        var score2 = deletes.Order().Take(1).First();

        Console.WriteLine($"advent7: 1 ({score1}), 2 ({score2})");
    }

    private static int GetDirSize(Dir dir)
    {
        var score = 0;
        foreach (var item in dir.Dirs)
        {
            score = GetDirSize(item.Value);
        }
        foreach (var item in dir.Fils)
        {
            score += item.Value.Size;
        }
        
        if (score < 100000 && dir.Parent != null) { score1 += score; }
        
        return score;
    }

    private static void PopulateBaseDir(string[] fileContent, Dir baseDir)
    {
        Dir currentDirectory = baseDir;
        for (int i = 0; i < fileContent.Length; i++)
        {
            var line = fileContent[i];
            if (line == "$ cd /")
            {
                currentDirectory = baseDir;
            }
            else if (line == "$ cd ..")
            {
                currentDirectory = currentDirectory.Parent;
            }
            else if (line[..4] == "$ cd")
            {
                // change dir
                var dirName = line[5..];
                currentDirectory = currentDirectory.Dirs[dirName];
            }
            else if (line[..4] == "$ ls")
            {
                // list dir, do nothing
            }
            else if (line[..3] == "dir")
            {
                // directory
                currentDirectory.Dirs.Add(line[4..], new Dir(line[4..], currentDirectory));
            }
            else
            {
                var f = RegexMatchFile().Match(line);
                currentDirectory.Fils.Add(f.Groups[2].Value, new Fil(f.Groups[2].Value, int.Parse(f.Groups[1].Value)));
            }
        }
    }

    private class Dir
    {
        private string Name { get; set; }
        public Dir Parent { get; set; }
        public Dictionary<string, Dir> Dirs { get; set; } = new();
        public Dictionary<string, Fil> Fils { get; set; } = new();

        public Dir(string name)
        {
            Name = name;
        }
        public Dir(string name, Dir parent) : this(name)
        {
            Parent = parent;
        }

        public int GetSize()
        {
            var score = Dirs.Values.Sum(x => x.GetSize());
            score += Fils.Values.Sum(x => x.Size);

            if (lack > 0 && score > lack)
            {
                deletes.Add(score);
            }
            return score;
        }
    }
    private class Fil
    {
        public string Name { get; set; }
        public int Size { get; set; }

        public Fil(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }

    [GeneratedRegex("(\\d+) ([\\w.]+)")]
    private static partial Regex RegexMatchFile();
}
