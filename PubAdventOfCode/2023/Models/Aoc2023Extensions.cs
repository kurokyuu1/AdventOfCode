using AdventOfCode._2023.Puzzles.DayEight;
using AdventOfCode._2023.Puzzles.DayFive;
using AdventOfCode._2023.Puzzles.DayFour;
using AdventOfCode._2023.Puzzles.DayNine;
using AdventOfCode._2023.Puzzles.DayOne;
using AdventOfCode._2023.Puzzles.DaySeven;
using AdventOfCode._2023.Puzzles.DaySix;
using AdventOfCode._2023.Puzzles.DayThree;
using AdventOfCode._2023.Puzzles.DayTwo;
using AdventOfCode.Core.Contracts;

namespace AdventOfCode._2023.Models
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
    }
}
