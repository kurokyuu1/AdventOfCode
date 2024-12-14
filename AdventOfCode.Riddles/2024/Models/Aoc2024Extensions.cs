#region "Usings"

using AdventOfCode.Core.Contracts;
using AdventOfCode.Riddles._2024.Puzzles.DayFive;
using AdventOfCode.Riddles._2024.Puzzles.DayFour;
using AdventOfCode.Riddles._2024.Puzzles.DayOne;
using AdventOfCode.Riddles._2024.Puzzles.DaySeven;
using AdventOfCode.Riddles._2024.Puzzles.DayThree;
using AdventOfCode.Riddles._2024.Puzzles.DayTwo;

#endregion

namespace AdventOfCode.Riddles._2024.Models;

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
        new SolutionDaySeven(),
    ];

    public static Dictionary<int, IAdventModule> ModulesFor2024Dictionary = new()
    {
        { 1, new SolutionDayOne() },
        { 2, new SolutionDayTwo() },
        { 3, new SolutionDayThree() },
        { 4, new SolutionDayFour() },
        { 5, new SolutionDayFive() },
        { 7, new SolutionDaySeven() },
    };

    #endregion
}
