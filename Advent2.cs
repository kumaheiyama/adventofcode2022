using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode1
{
    internal class Advent2
    {
        internal static async Task Run()
        {
            var fileContent = await File.ReadAllLinesAsync("input2.txt");

            var part1score = 0;
            for (int i = 0; i < fileContent.Length; i++)
            {
                // A for Rock, B for Paper, and C for Scissors
                // X for Rock, Y for Paper, and Z for Scissors
                // Points: 1 for Rock, 2 for Paper, and 3 for Scissors)
                // Outcome of the round: 0 if you lost, 3 if the round was a draw, and 6 if you won
                var line = fileContent[i];
                switch (line)
                {
                    case "A X":
                        // draw
                        part1score += 1 + 3;
                        break;
                    case "A Y":
                        // win
                        part1score += 2 + 6;
                        break;
                    case "A Z":
                        // loss
                        part1score += 3 + 0;
                        break;
                    case "B X":
                        // loss
                        part1score += 1 + 0;
                        break;
                    case "B Y":
                        // draw
                        part1score += 2 + 3;
                        break;
                    case "B Z":
                        // win
                        part1score += 3 + 6;
                        break;
                    case "C X":
                        // win
                        part1score += 1 + 6;
                        break;
                    case "C Y":
                        // loss
                        part1score += 2 + 0;
                        break;
                    case "C Z":
                        // draw
                        part1score += 3 + 3;
                        break;
                }
            }
            
            var part2score = 0;
            for (int i = 0; i < fileContent.Length; i++)
            {
                // A for Rock, B for Paper, and C for Scissors
                // X for lose, Y for draw, and Z for win
                // Points: 1 for Rock, 2 for Paper, and 3 for Scissors)
                // Outcome of the round: 0 if you lost, 3 if the round was a draw, and 6 if you won
                var line = fileContent[i];
                switch (line)
                {
                    case "A X":
                        // lose
                        part2score += 3 + 0;
                        break;
                    case "A Y":
                        // draw
                        part2score += 1 + 3;
                        break;
                    case "A Z":
                        // win
                        part2score += 2 + 6;
                        break;
                    case "B X":
                        // lose
                        part2score += 1 + 0;
                        break;
                    case "B Y":
                        // draw
                        part2score += 2 + 3;
                        break;
                    case "B Z":
                        // win
                        part2score += 3 + 6;
                        break;
                    case "C X":
                        // lose
                        part2score += 2 + 0;
                        break;
                    case "C Y":
                        // draw
                        part2score += 3 + 3;
                        break;
                    case "C Z":
                        // win
                        part2score += 1 + 6;
                        break;
                }
            }

            Console.WriteLine($"Advent2: 1 ({part1score}), 2 ({part2score})");
        }
    }
}
