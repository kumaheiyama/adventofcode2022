using System.Text.RegularExpressions;

internal class Advent10
{
    internal async static Task Run()
    {
        var fileContent = await File.ReadAllLinesAsync("input10.txt");

        int score1 = 0, score2 = 0;
        var x = 1;
        var ops = new Dictionary<int, int>();
        var cycleTime = 1;
        for (int i = 0; i < fileContent.Length; i++)
        {
            var line = fileContent[i];

            switch (line[..4])
            {
                case "noop":
                    cycleTime++;
                    break;
                case "addx":
                    var y = int.Parse(line[5..].Trim());
                    cycleTime += 2;
                    ops.Add(cycleTime, y);
                    break;
            }
        }

        cycleTime = 1;
        var crtTime = 0;
        do
        {
            if (ops.TryGetValue(cycleTime, out int pl))
            {
                x += pl;
            }
            if (cycleTime == 20 || cycleTime == 60 || cycleTime == 100 || cycleTime == 140 || cycleTime == 180 || cycleTime == 220)
            {
                var signalStrength = x * cycleTime;
                score1 += signalStrength;
            }

            if (crtTime >= x - 1 && crtTime <= x + 1)
            {
                Console.Write("#");
            }
            else
            {
                Console.Write(".");
            }
            if (crtTime == 39)
            {
                Console.WriteLine();
            }

            cycleTime++;
            crtTime++;
            if (crtTime == 40) { crtTime = 0; }
        } while (cycleTime < 300);
        Console.WriteLine();

        Console.WriteLine($"Advent10: 1 ({score1}), 2 ({score2})");
    }
}
