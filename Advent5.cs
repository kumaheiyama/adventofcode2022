using System.Text.RegularExpressions;

internal partial class Advent5
{
    internal async static Task Run()
    {
        var fileContent = await File.ReadAllLinesAsync("input5.txt");

        var stacks = GetStacks();

        string topOfStack = "", topOfStack2 = "";
        var regx = Regx();
        for (int i = 0; i < fileContent.Length; i++)
        {
            var match = regx.Match(fileContent[i]);
            var count = int.Parse(match.Groups[1].Value);
            var from = int.Parse(match.Groups[2].Value);
            var to = int.Parse(match.Groups[3].Value);

            var stackFrom = stacks[from];
            var stackTo = stacks[to];
            for (int j = 0; j < count; j++)
            {
                var pop = stackFrom.Pop();
                stackTo.Push(pop);
            }
        }


        for (int i = 1; i <= stacks.Count; i++)
        {
            topOfStack += stacks[i].Pop();
        }

        stacks = GetStacks();

        for (int i = 0; i < fileContent.Length; i++)
        {
            var match = regx.Match(fileContent[i]);
            var count = int.Parse(match.Groups[1].Value);
            var from = int.Parse(match.Groups[2].Value);
            var to = int.Parse(match.Groups[3].Value);

            var stackFrom = stacks[from];
            var stackTo = stacks[to];
            var tempStack = new Stack<string>();
            for (int j = 0; j < count; j++)
            {
                var pop = stackFrom.Pop();
                tempStack.Push(pop);
            }
            for (int j = 0; j < count; j++)
            {
                var pop = tempStack.Pop();
                stackTo.Push(pop);
            }
        }

        for (int i = 1; i <= stacks.Count; i++)
        {
            topOfStack2 += stacks[i].Pop();
        }

        Console.WriteLine($"Advent5: 1 ({topOfStack}), 2 ({topOfStack2})");
    }

    /*
                    [B] [L]     [J]    
                [B] [Q] [R]     [D] [T]
                [G] [H] [H] [M] [N] [F]
            [J] [N] [D] [F] [J] [H] [B]
        [Q] [F] [W] [S] [V] [N] [F] [N]
    [W] [N] [H] [M] [L] [B] [R] [T] [Q]
    [L] [T] [C] [R] [R] [J] [W] [Z] [L]
    [S] [J] [S] [T] [T] [M] [D] [B] [H]
     1   2   3   4   5   6   7   8   9 
    */
    private static Dictionary<int, Stack<string>> GetStacks()
    {
        var stacks = new Dictionary<int, Stack<string>>
        {
            //{ 1, new Stack<string>(new string[] { "W", "L", "S" }) },
            { 1, new Stack<string>(new string[] { "S", "L", "W" }) },
            //{ 2, new Stack<string>(new string[] { "Q", "N", "T", "J" }) },
            { 2, new Stack<string>(new string[] { "J", "T", "N", "Q" }) },
            //{ 3, new Stack<string>(new string[] { "J", "F", "H", "C", "S" }) },
            { 3, new Stack<string>(new string[] { "S", "C", "H", "F", "J" }) },
            //{ 4, new Stack<string>(new string[] { "B", "G", "N", "W", "M", "R", "T" }) },
            { 4, new Stack<string>(new string[] { "T", "R", "M", "W", "N", "G", "B" }) },
            //{ 5, new Stack<string>(new string[] { "B", "Q", "H", "D", "S", "L", "R", "T"}) },
            { 5, new Stack<string>(new string[] { "T", "R", "L", "S", "D", "H", "Q", "B"}) },
            //{ 6, new Stack<string>(new string[] { "L", "R", "H", "F", "V", "B", "J", "M" }) },
            { 6, new Stack<string>(new string[] { "M", "J", "B", "V", "F", "H", "R", "L" }) },
            //{ 7, new Stack<string>(new string[] { "M", "J", "N", "R", "W", "D" }) },
            { 7, new Stack<string>(new string[] { "D", "W", "R", "N", "J", "M" }) },
            //{ 8, new Stack<string>(new string[] { "J", "D", "N", "H", "F", "T", "Z", "B" }) },
            { 8, new Stack<string>(new string[] { "B", "Z", "T", "F", "H", "N", "D", "J" }) },
            //{ 9, new Stack<string>(new string[] { "T", "F", "B", "N", "Q", "L", "H" }) }
            { 9, new Stack<string>(new string[] { "H", "L", "Q", "N", "B", "F", "T" }) }
        };

        return stacks;
    }

    [GeneratedRegex("move\\s(\\d+)\\sfrom\\s(\\d+)\\sto\\s(\\d+)")]
    private static partial Regex Regx();
}
