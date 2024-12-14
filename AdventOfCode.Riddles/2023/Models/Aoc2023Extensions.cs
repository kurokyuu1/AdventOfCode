using AdventOfCode.Core.Contracts;
using AdventOfCode.Riddles._2023.Puzzles.DayEight;
using AdventOfCode.Riddles._2023.Puzzles.DayFive;
using AdventOfCode.Riddles._2023.Puzzles.DayFour;
using AdventOfCode.Riddles._2023.Puzzles.DayNine;
using AdventOfCode.Riddles._2023.Puzzles.DayOne;
using AdventOfCode.Riddles._2023.Puzzles.DaySeven;
using AdventOfCode.Riddles._2023.Puzzles.DaySix;
using AdventOfCode.Riddles._2023.Puzzles.DayThree;
using AdventOfCode.Riddles._2023.Puzzles.DayTwo;

namespace AdventOfCode.Riddles._2023.Models
{
    public static class Aoc2023Extensions
    {
        public static List<IAdventModule> ModulesFor2023 =>
        [
            new SolutionDayOne(),
            new SolutionDayTwo(),
            new SolutionDayThree(),
            new SolutionDayFour(),
            new SolutionDayFive(),
            new SolutionDaySix(),
            new SolutionDaySeven(),
            new SolutionDayEight(),
            new SolutionDayNine(),
        ];

        public static Dictionary<int, IAdventModule> ModulesFor2023Dictionary = new()
        {
            { 1, new SolutionDayOne() },
            { 2, new SolutionDayTwo() },
            { 3, new SolutionDayThree() },
            { 4, new SolutionDayFour() },
            { 5, new SolutionDayFive() },
            { 6, new SolutionDaySix() },
            { 7, new SolutionDaySeven() },
            { 8, new SolutionDayEight() },
            { 9, new SolutionDayNine() },
        };
    }
}
