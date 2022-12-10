using System.Text.RegularExpressions;

internal partial class Advent9
{
    private static readonly Dictionary<int, Dictionary<int, int>> table = new();
    private static Tuple<int, int> headPos = new(499, 499);
    private static Tuple<int, int> tailPos = new(499, 499);

    internal async static Task Run()
    {
        var fileContent = await File.ReadAllLinesAsync("input9.txt");

        for (int i = 0; i <= 999; i++)
        {
            table[i] = new Dictionary<int, int>();
        }
        for (int i = 0; i <= 999; i++)
        {
            var row = table[i];
            for (int j = 0; j <= 999; j++)
            {
                row[j] = 0;
            }
        }
        table[499][499] = 1;

        int score1 = 0, score2 = 0;
        for (int i = 0; i < fileContent.Length; i++)
        {
            var line = MapDirAndDist().Match(fileContent[i]);
            var dir = line.Groups["dir"].Value;
            var dist = int.Parse(line.Groups["dist"].Value);

            switch (dir)
            {
                case "D":
                    MoveDown(dist);
                    break;
                case "U":
                    MoveUp(dist);
                    break;
                case "L":
                    MoveLeft(dist);
                    break;
                case "R":
                    MoveRight(dist);
                    break;
            }
        }

        for (int i = 0; i < table.Count; i++)
        {
            var line = table[i];
            for (int j = 0; j < line.Count; j++)
            {
                if (line[j] > 0) score1++;
            }
        }

        Console.WriteLine($"Advent9: 1 ({score1}), 2 ({score2})");
    }

    private static void MoveRight(int dist)
    {
        var currentHeadPos = headPos;
        var currentTailPos = tailPos;
        for (int i = 0; i < dist; i++)
        {
            currentHeadPos = new Tuple<int, int>(currentHeadPos.Item1, currentHeadPos.Item2 + 1);
            var topChange = currentHeadPos.Item1 - currentTailPos.Item1;
            var leftChange = currentHeadPos.Item2 - currentTailPos.Item2;
            var tp = CalcTailPos(currentTailPos, topChange, leftChange);
            if (tailPos.Item1 != tp.Item1 || tailPos.Item2 != tp.Item2)
            {
                currentTailPos = new Tuple<int, int>(tp.Item1, tp.Item2);
                table[currentTailPos.Item1][currentTailPos.Item2]++;
            }
        }
        headPos = currentHeadPos;
        tailPos = currentTailPos;
    }

    private static void MoveLeft(int dist)
    {
        var currentHeadPos = headPos;
        var currentTailPos = tailPos;
        for (int i = 0; i < dist; i++)
        {
            currentHeadPos = new Tuple<int, int>(currentHeadPos.Item1, currentHeadPos.Item2 - 1);
            var topChange = currentHeadPos.Item1 - currentTailPos.Item1;
            var leftChange = currentHeadPos.Item2 - currentTailPos.Item2;
            var tp = CalcTailPos(currentTailPos, topChange, leftChange);
            if (tailPos.Item1 != tp.Item1 || tailPos.Item2 != tp.Item2)
            {
                currentTailPos = new Tuple<int, int>(tp.Item1, tp.Item2);
                table[currentTailPos.Item1][currentTailPos.Item2]++;
            }
        }
        headPos = currentHeadPos;
        tailPos = currentTailPos;
    }

    private static void MoveUp(int dist)
    {
        var currentHeadPos = headPos;
        var currentTailPos = tailPos;
        for (int i = 0; i < dist; i++)
        {
            currentHeadPos = new Tuple<int, int>(currentHeadPos.Item1 - 1, currentHeadPos.Item2);
            var topChange = currentHeadPos.Item1 - currentTailPos.Item1;
            var leftChange = currentHeadPos.Item2 - currentTailPos.Item2;
            var tp = CalcTailPos(currentTailPos, topChange, leftChange);
            if (tailPos.Item1 != tp.Item1 || tailPos.Item2 != tp.Item2)
            {
                currentTailPos = new Tuple<int, int>(tp.Item1, tp.Item2);
                table[currentTailPos.Item1][currentTailPos.Item2]++;
            }
        }
        headPos = currentHeadPos;
        tailPos = currentTailPos;
    }

    private static void MoveDown(int dist)
    {
        var currentHeadPos = headPos;
        var currentTailPos = tailPos;
        for (int i = 0; i < dist; i++)
        {
            currentHeadPos = new Tuple<int, int>(currentHeadPos.Item1 + 1, currentHeadPos.Item2);
            var topChange = currentHeadPos.Item1 - currentTailPos.Item1;
            var leftChange = currentHeadPos.Item2 - currentTailPos.Item2;
            var tp = CalcTailPos(currentTailPos, topChange, leftChange);
            if (tailPos.Item1 != tp.Item1 || tailPos.Item2 != tp.Item2)
            {
                currentTailPos = new Tuple<int, int>(tp.Item1, tp.Item2);
                table[currentTailPos.Item1][currentTailPos.Item2]++;
            }
        }
        headPos = currentHeadPos;
        tailPos = currentTailPos;
    }

    private static Tuple<int, int> CalcTailPos(Tuple<int, int> tailPos, int topChange, int leftChange)
    {
        //[01] [02] [03] [04] [05]
        //[06] [07] [08] [09] [10]
        //[11] [12] [XX] [14] [15]
        //[16] [17] [18] [19] [20]
        //[21] [22] [23] [24] [25]

        switch (topChange)
        {
            case -2:
                //01 - 05
                switch (leftChange)
                {
                    case -2: //01
                        tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2 - 1);
                        break;
                    case -1: //02
                        tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2 - 1);
                        break;
                    case 0:  //03
                        tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2);
                        break;
                    case 1:  //04
                        tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2 + 1);
                        break;
                    case 2:  //05
                        tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2 + 1);
                        break;
                }
                break;
            case -1:
                //06 - 10
                switch (leftChange)
                {
                    case -2: //06
                        tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2 - 1);
                        break;
                    //case -1: //07
                    //    tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2 - 1);
                    //    break;
                    //case 0:  //08
                    //    tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2);
                    //    break;
                    //case 1:  //09
                    //    tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2 + 1);
                    //    break;
                    case 2:  //10
                        tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2 + 1);
                        break;
                }
                break;
            case 0:
                //11 - 15
                switch (leftChange)
                {
                    case -2: //11
                        tailPos = new Tuple<int, int>(tailPos.Item1, tailPos.Item2 - 1);
                        break;
                    //case -1: //12
                    //    tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2 - 1);
                    //    break;
                    //case 0:  //13
                    //    tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2);
                    //    break;
                    //case 1:  //14
                    //    tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2 + 1);
                    //    break;
                    case 2:  //15
                        tailPos = new Tuple<int, int>(tailPos.Item1, tailPos.Item2 + 1);
                        break;
                }
                break;
            case 1:
                //16 - 20
                switch (leftChange)
                {
                    case -2: //16
                        tailPos = new Tuple<int, int>(tailPos.Item1 + 1, tailPos.Item2 - 1);
                        break;
                    //case -1: //17
                    //    tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2 - 1);
                    //    break;
                    //case 0:  //18
                    //    tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2);
                    //    break;
                    //case 1:  //19
                    //    tailPos = new Tuple<int, int>(tailPos.Item1 - 1, tailPos.Item2 + 1);
                    //    break;
                    case 2:  //20
                        tailPos = new Tuple<int, int>(tailPos.Item1 + 1, tailPos.Item2 + 1);
                        break;
                }
                break;
            case 2:
                //21 - 25
                switch (leftChange)
                {
                    case -2: //21
                        tailPos = new Tuple<int, int>(tailPos.Item1 + 1, tailPos.Item2 - 1);
                        break;
                    case -1: //22
                        tailPos = new Tuple<int, int>(tailPos.Item1 + 1, tailPos.Item2 - 1);
                        break;
                    case 0:  //23
                        tailPos = new Tuple<int, int>(tailPos.Item1 + 1, tailPos.Item2);
                        break;
                    case 1:  //24
                        tailPos = new Tuple<int, int>(tailPos.Item1 + 1, tailPos.Item2 + 1);
                        break;
                    case 2:  //25
                        tailPos = new Tuple<int, int>(tailPos.Item1 + 1, tailPos.Item2 + 1);
                        break;
                }
                break;
        }

        return tailPos;
    }

    [GeneratedRegex("(?<dir>[A-Z]{1}) (?<dist>[\\d]{1,})")]
    private static partial Regex MapDirAndDist();
}
