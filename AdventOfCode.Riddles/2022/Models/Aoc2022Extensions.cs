#region "Usings"

using AdventOfCode.Core.Contracts;
using AdventOfCode.Riddles._2022.Puzzles.DayEleven;
using AdventOfCode.Riddles._2022.Puzzles.DayFive;
using AdventOfCode.Riddles._2022.Puzzles.DayFour;
using AdventOfCode.Riddles._2022.Puzzles.DayNine;
using AdventOfCode.Riddles._2022.Puzzles.DayOne;
using AdventOfCode.Riddles._2022.Puzzles.DaySix;
using AdventOfCode.Riddles._2022.Puzzles.DayThirteen;
using AdventOfCode.Riddles._2022.Puzzles.DayThree;
using AdventOfCode.Riddles._2022.Puzzles.DayTwentyFive;
using AdventOfCode.Riddles._2022.Puzzles.DayTwo;

#endregion

namespace AdventOfCode.Riddles._2022.Models;

public static class Aoc2022Extensions
{
    #region "Properties"

    public static List<IAdventModule> ModulesFor2022 =>
    [
        new SolutionDayOne(),
        new SolutionDayTwo(),
        new SolutionDayThree(),
        new SolutionDayFour(),
        new SolutionDayFive(),
        new SolutionDaySix(),
        new SolutionDayEleven(),
        new SolutionDayThirteen(),
        new SolutionTwentyFive(),
        new SolutionDayNine(),
    ];

    public static Dictionary<int, IAdventModule> ModulesFor2022Dictionary = new()
            {
        { 1, new SolutionDayOne() },
        { 2, new SolutionDayTwo() },
        { 3, new SolutionDayThree() },
        { 4, new SolutionDayFour() },
        { 5, new SolutionDayFive() },
        { 6, new SolutionDaySix() },
        { 9, new SolutionDayNine() },
        { 11, new SolutionDayEleven() },
        { 13, new SolutionDayThirteen() },
        { 25, new SolutionTwentyFive() },
    };

    #endregion
}
