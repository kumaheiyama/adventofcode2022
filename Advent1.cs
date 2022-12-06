namespace AdventOfCode1
{
    internal class Advent1
    {
        internal static async Task Run()
        {
            //https://adventofcode.com/2022/day/1/input

            var fileContent = await File.ReadAllLinesAsync("input1.txt");

            var elfs = new Dictionary<int, int>();
            var i = 1;
            elfs.Add(i, 0);
            var currentElf = i;
            foreach (var line in fileContent)
            {
                if (line.Length == 0)
                {
                    elfs.Add(++i, 0);
                    currentElf = i;
                    continue;
                }

                var intLine = int.Parse(line);
                elfs[currentElf] += intLine;
            }

            var orderedElfs = elfs.OrderByDescending(x => x.Value);
            var highestElf = orderedElfs.First();

            var topThree = orderedElfs.Take(3);
            var topThreeSum = topThree.Sum(x => x.Value);
            Console.WriteLine($"Advent1: 1 ({highestElf.Value}), 2 ({topThreeSum})");
        }
    }
}
