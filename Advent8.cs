internal class Advent8
{
    private static readonly Dictionary<int, Dictionary<int, int>> treeGrid = new();


    internal async static Task Run()
    {
        var fileContent = await File.ReadAllLinesAsync("input8.txt");

        for (int i = 0; i < fileContent.Length; i++)
        {
            var line = fileContent[i];
            treeGrid[i] = new Dictionary<int, int>();
            for (int j = 0; j < line.Length; j++)
            {
                treeGrid[i][j] = int.Parse(line[j].ToString());
            }
        }

        int score1 = 0, score2 = 0;
        for (int i = 1; i < treeGrid.Count - 1; i++)
        {
            var line = treeGrid[i];
            for (int j = 1; j < line.Count - 1; j++)
            {
                var cell = treeGrid[i][j];
                var visTop = IsVisibleTop(cell, i - 1, j);
                var visRight = IsVisibleRight(cell, i, j + 1);
                var visBottom = IsVisibleBottom(cell, i + 1, j);
                var visLeft = IsVisibleLeft(cell, i, j - 1);

                if (visTop || visRight || visBottom || visLeft)
                {
                    score1++;
                }
            }
        }
        score1 += fileContent.Length * 2 + fileContent[0].Length * 2 - 4;

        Console.WriteLine($"Advent8: 1 ({score1}), 2 ({score2})");
    }

    private static bool IsVisibleLeft(int height, int top, int left)
    {
        var visible = false;
        for (int i = left; i >= 0; i--)
        {
            if (treeGrid[top][i] >= height) { visible = false; break; }
            if (treeGrid[top][i] < height) { visible = true; }
        }
        return visible;
    }

    private static bool IsVisibleBottom(int height, int top, int left)
    {
        var visible = false;
        for (int i = top; i < treeGrid.Count; i++)
        {
            if (treeGrid[i][left] >= height) { visible = false; break; }
            if (treeGrid[i][left] < height) { visible = true; }
        }
        return visible;
    }

    private static bool IsVisibleRight(int height, int top, int left)
    {
        var visible = false;
        for (int i = left; i < treeGrid[top].Count; i++)
        {
            if (treeGrid[top][i] >= height) { visible = false; break; }
            if (treeGrid[top][i] < height) { visible = true; }
        }
        return visible;
    }

    private static bool IsVisibleTop(int height, int top, int left)
    {
        var visible = false;
        for (int i = top; i >= 0; i--)
        {
            if (treeGrid[i][left] >= height) { visible = false; break; }
            if (treeGrid[i][left] < height) { visible = true; }
        }
        return visible;
    }
}
