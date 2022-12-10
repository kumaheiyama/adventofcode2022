using System.Text.RegularExpressions;

internal partial class Advent9
{
    internal async static Task Run()
    {
        var fileContent = await File.ReadAllLinesAsync("input9.txt");

        var score1 = GetRopeTailPos(fileContent, 2);
        var score2 = GetRopeTailPos(fileContent, 10);

        Console.WriteLine($"Advent9: 1 ({score1}), 2 ({score2})");
    }

    private static object GetRopeTailPos(string[] fileContent, int len)
    {
        (int x, int y)[] snakePos = new (int x, int y)[len];
        HashSet<(int, int)> visitedPos = new();

        for (int i = 0; i < fileContent.Length; i++)
        {
            var line = MapDirAndDist().Match(fileContent[i]);
            var dir = line.Groups["dir"].Value;
            var dist = int.Parse(line.Groups["dist"].Value);

            for (int j = 0; j < dist; j++)
            {
                switch (dir)
                {
                    case "R":
                        snakePos[0].x += 1;
                        break;
                    case "L":
                        snakePos[0].x -= 1;
                        break;
                    case "U":
                        snakePos[0].y -= 1;
                        break;
                    case "D":
                        snakePos[0].y += 1;
                        break;
                }

                for (int k = 1; k < len; k++)
                {
                    int dx = snakePos[k - 1].x - snakePos[k].x;
                    int dy = snakePos[k - 1].y - snakePos[k].y;

                    if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
                    {
                        snakePos[k].x += Math.Sign(dx);
                        snakePos[k].y += Math.Sign(dy);
                    }
                }

                visitedPos.Add(snakePos[len - 1]);
            }
        }
        return visitedPos.Count;
    }

    [GeneratedRegex("(?<dir>[A-Z]{1}) (?<dist>[\\d]{1,})")]
    private static partial Regex MapDirAndDist();
}
