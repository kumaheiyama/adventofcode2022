using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode1
{
    internal class Advent3
    {
        internal static async Task Run()
        {
            var fileContent = await File.ReadAllLinesAsync("input3.txt");

            var values = GetValues();

            var score = 0;
            for (int i = 0; i < fileContent.Length; i++)
            {
                var line = fileContent[i];
                var c1 = new string(line.Substring(0, line.Length / 2).Distinct().ToArray());
                var c2 = line.Substring(line.Length / 2).Distinct().ToArray();

                var commonLetter = "";
                for (int j = 0; j < c2.Length; j++)
                {
                    var l = c2[j];
                    if (c1.Contains(l))
                    {
                        score += values[l];
                        commonLetter = l.ToString();
                        break;
                    }
                }

                //Console.WriteLine($"{line} - {c1} - {c2} {commonLetter}: {score}");
            }

            var score2 = 0;
            for (int i = 0; i < fileContent.Length; i += 6)
            {
                var c1 = new string(fileContent[i].Distinct().ToArray());
                var c2 = fileContent[i + 1].Distinct().ToArray();
                var c3 = fileContent[i + 2].Distinct().ToArray();

                var commonLetter = GetCommonLetter(c1, c2, c3);
                score2 += values[commonLetter];
                //Console.WriteLine($"{c1} - {new string(c2)} - {new string(c3)} - {commonLetter}: {score2}");

                var c4 = new string(fileContent[i + 3].Distinct().ToArray());
                var c5 = fileContent[i + 4].Distinct().ToArray();
                var c6 = fileContent[i + 5].Distinct().ToArray();

                var commonLetter2 = GetCommonLetter(c4, c5, c6);
                score2 += values[commonLetter2];
                //Console.WriteLine($"{c4} - {new string(c5)} - {new string(c6)} - {commonLetter2}: {score2}");
            }

            Console.WriteLine($"Advent3: 1 ({score}), 2 ({score2})");
        }

        private static char GetCommonLetter(string c1, char[] c2, char[] c3)
        {
            string commonLetters = "";
            for (int j = 0; j < c2.Length; j++)
            {
                var l = c2[j];
                if (c1.Contains(l))
                {
                    commonLetters += l;
                }
            }

            for (int j = 0; j < c3.Length; j++)
            {
                var l = c3[j];
                if (commonLetters.Contains(l))
                {
                    return l;
                }
            }

            throw new Exception();
        }

        /// <summary>
        /// Lowercase item types a through z have priorities 1 through 26.
        /// Uppercase item types A through Z have priorities 27 through 52.
        /// </summary>
        /// <returns></returns>
        private static Dictionary<char, int> GetValues()
        {
            var values = new Dictionary<char, int>();
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < chars.Length; i++)
            {
                var ch = chars[i];
                values.Add(ch, i + 1);
                //Console.WriteLine($"{ch}: {i + 1}");
            }
            return values;
        }
    }
}
