internal class Advent4
{
    internal async static Task Run()
    {
        var fileContent = await File.ReadAllLinesAsync("input4.txt");

        var getRange = (string section) =>
        {
            var start = int.Parse(section.Split('-')[0]);
            var end = int.Parse(section.Split('-')[1]);

            return new { Start = start, End = end };
        };

        int score1 = 0, score2 = 0;
        for (int i = 0; i < fileContent.Length; i++)
        {
            var line = fileContent[i];
            var section1 = line.Split(',')[0];
            var section2 = line.Split(',')[1];

            var range1 = getRange(section1);
            var range2 = getRange(section2);

            if ((range1.Start >= range2.Start && range1.End <= range2.End)
                || (range2.Start >= range1.Start && range2.End <= range1.End))
            {
                score1++;
            }

            /*
            5-7,7-9 overlaps in a single section, 7.
            2-8,3-7 overlaps all of the sections 3 through 7.
            6-6,4-6 overlaps in a single section, 6.
            2-6,4-8 overlaps in sections 4, 5, and 6.
             */

            if (
                (range1.End >= range2.Start && range1.Start <= range2.Start)
                || (range1.End >= range2.Start && range1.End <= range2.End)
                || (range2.End >= range1.Start && range2.Start <= range1.Start)
                || (range2.End >= range1.Start && range2.End <= range1.End)

                )
            {
                score2++;
            }
        }

        Console.WriteLine($"Advent4: 1 ({score1}), 2 ({score2})");
    }
}