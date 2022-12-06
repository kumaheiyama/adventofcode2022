internal class Advent7
{
    internal async static Task Run()
    {
        var fileContent = await File.ReadAllLinesAsync("input7.txt");

        int score1 = 0, score2 = 0;
        for (int i = 0; i < fileContent.Length; i++)
        {

        }

        Console.WriteLine($"Advent7: 1 ({score1}), 2 ({score2})");
    }
}
