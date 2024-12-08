#region "Usings"

using AdventOfCode._2024.Puzzles.DayFive;
using AdventOfCode._2024.Puzzles.DayFour;
using AdventOfCode._2024.Puzzles.DayOne;
using AdventOfCode._2024.Puzzles.DaySix;
using AdventOfCode._2024.Puzzles.DayThree;
using AdventOfCode._2024.Puzzles.DayTwo;
using AdventOfCode.Core.Contracts;

#endregion

namespace AdventOfCode._2024.Models;

public static class Aoc2024Extensions
{
    #region "Properties"

    public static List<IAdventModule> ModulesFor2024 =>
    [
        new SolutionDayOne(),
        new SolutionDayTwo(),
        new SolutionDayThree(),
        new SolutionDayFour(),
        new SolutionDayFive(),
        new SolutionDaySix(),
    ];

    #endregion
}
