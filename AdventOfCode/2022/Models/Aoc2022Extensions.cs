#region "Usings"

using AdventOfCode._2022.Puzzles.DayEleven;
using AdventOfCode._2022.Puzzles.DayFive;
using AdventOfCode._2022.Puzzles.DayFour;
using AdventOfCode._2022.Puzzles.DayNine;
using AdventOfCode._2022.Puzzles.DayOne;
using AdventOfCode._2022.Puzzles.DaySix;
using AdventOfCode._2022.Puzzles.DayThirteen;
using AdventOfCode._2022.Puzzles.DayThree;
using AdventOfCode._2022.Puzzles.DayTwentyFive;
using AdventOfCode._2022.Puzzles.DayTwo;
using AdventOfCode.Core.Contracts;

#endregion

namespace AdventOfCode._2022.Models;

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

    #endregion
}
