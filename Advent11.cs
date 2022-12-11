using System.Text.RegularExpressions;

internal partial class Advent11
{
    private static Dictionary<int, Monkey> monkeys = new();
    private static Dictionary<int, Monkey> monkeys2 = new();

    internal async static Task Run()
    {
        var fileContent = await File.ReadAllLinesAsync("input11.txt");

        double score1 = 0, score2 = 0;
        for (var i = 0; i < fileContent.Length; i += 7)
        {
            var line1 = fileContent[i];
            var line2 = fileContent[i + 1];
            var line3 = fileContent[i + 2];
            var line4 = fileContent[i + 3];
            var line5 = fileContent[i + 4];
            var line6 = fileContent[i + 5];

            var monkey = int.Parse(line1[7].ToString());
            var itemsStr = MatchItems().Match(line2).Groups[1].Value;
            var items = itemsStr.Split(',').Select(int.Parse).ToList();
            var op = MatchOp().Match(line3).Groups["op"].Value;
            var opValStr = MatchOp().Match(line3).Groups["dest"].Value;
            var opVal = opValStr == "old" ? -1 : int.Parse(opValStr);
            var test = int.Parse(MatchTest().Match(line4).Groups[1].Value);
            var nextTrue = int.Parse(MatchNextTrue().Match(line5).Groups[1].Value);
            var nextFalse = int.Parse(MatchNextFalse().Match(line6).Groups[1].Value);

            monkeys.Add(monkey, new Monkey(monkey, items, op, opVal, test, nextTrue, nextFalse, 0));
            monkeys2.Add(monkey, new Monkey(monkey, items, op, opVal, test, nextTrue, nextFalse, 0));
        }

        for (int round = 0; round < 20; round++)
        {
            for (int i = 0; i < monkeys.Count; i++)
            {
                var monkey = monkeys[i];
                if (monkey.items.Count == 0) continue;

                HandleThrow(monkey);
            }
        }

        score1 = monkeys
            .OrderByDescending(x => x.Value.timesInspected)
            .Take(2)
            .Select(x => x.Value.timesInspected)
            .Aggregate((a, x) => a * x);

        for (int round = 0; round < 1000; round++)
        {
            Console.WriteLine($"== After round {round +1} ==");
            for (int i = 0; i < monkeys2.Count; i++)
            {
                var monkey = monkeys2[i];

                HandleThrow2(monkey);
                Console.WriteLine($"Monkey {monkey.monkey} inspected items {monkey.timesInspected} times.");
            }
        }

        score2 = monkeys2
            .OrderByDescending(x => x.Value.timesInspected)
            .Take(2)
            .Select(x => x.Value.timesInspected)
            .Aggregate((a, x) => a * x);

        Console.WriteLine($"Advent11: 1 ({score1}), 2 ({score2})");
    }

    private static void HandleThrow(Monkey monkey)
    {
        for (int i = 0; i < monkey.items.Count; i++)
        {
            monkey.timesInspected++;

            var worry = monkey.items[i];
            if (monkey.opVal == -1)
            {
                worry *= worry;
            }
            else if (monkey.op == "*")
            {
                worry *= monkey.opVal;
            }
            else if (monkey.op == "+")
            {
                worry += monkey.opVal;
            }

            worry = Convert.ToInt32(Math.Floor(Convert.ToDecimal(worry /= 3)));

            if (worry % monkey.test == 0)
            {
                monkeys[monkey.nextTrue].items.Add(worry);
            }
            else
            {
                monkeys[monkey.nextFalse].items.Add(worry);
            }
        }
        monkey.items = new List<int>();
    }

    private static void HandleThrow2(Monkey monkey)
    {
        for (int i = 0; i < monkey.items.Count; i++)
        {
            monkey.timesInspected++;

            var worry = monkey.items[i];
            if (monkey.opVal == -1)
            {
                worry *= worry;
            }
            else if (monkey.op == "*")
            {
                worry *= monkey.opVal;
            }
            else if (monkey.op == "+")
            {
                worry += monkey.opVal;
            }

            //worry = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(worry /= monkey.test)));

            if (worry % monkey.test == 0)
            {
                monkeys2[monkey.nextTrue].items.Add(worry);
            }
            else
            {
                monkeys2[monkey.nextFalse].items.Add(worry);
            }
        }
        monkey.items = new List<int>();
    }

    [GeneratedRegex("  Starting items: ([\\d, ]+)")]
    private static partial Regex MatchItems();
    [GeneratedRegex("  Operation: new = old (?<op>[*+]{1}) (?<dest>[\\d]+|old)")]
    private static partial Regex MatchOp();
    [GeneratedRegex("  Test: divisible by ([\\d]+)")]
    private static partial Regex MatchTest();
    [GeneratedRegex("    If true: throw to monkey ([\\d]+)")]
    private static partial Regex MatchNextTrue();
    [GeneratedRegex("    If false: throw to monkey ([\\d]+)")]
    private static partial Regex MatchNextFalse();
}

internal class Monkey
{
    public Monkey(int monkey, List<int> items, string op, int opVal, int test, int nextTrue, int nextFalse, int v)
    {
        this.monkey = monkey;
        this.items = items;
        this.op = op;
        this.opVal = opVal;
        this.test = test;
        this.nextTrue = nextTrue;
        this.nextFalse = nextFalse;
    }

    public int monkey { get; set; }
    public List<int> items { get; set; }
    public string op { get; set; }
    public int opVal { get; set; }
    public int test { get; set; }
    public int nextTrue { get; set; }
    public int nextFalse { get; set; }
    public double timesInspected { get; set; }
}
