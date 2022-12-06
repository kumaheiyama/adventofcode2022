internal class Advent6
{
    internal async static Task Run()
    {
        var fileContent = await File.ReadAllTextAsync("input6.txt");

        var score1 = GetScore(fileContent, 4);
        var score2 = GetScore(fileContent, 14);

        Console.WriteLine($"Advent6: 1 ({score1}), 2 ({score2})");
    }

    private static int GetScore(string input, int offset)
    {
        for (int i = 0; i < input.Length; i++)
        {
            var pos = GetPosition(input, i, offset);
            if (pos > 0)
            {
                return pos;
            }
        }
        return -1;
    }

    private static int GetPosition(string input, int index, int offset)
    {
        var total = string.Join("", input.Substring(index, offset).Distinct().ToArray());
        return (total.Length == offset) ? index + offset : -1;
    }
}
