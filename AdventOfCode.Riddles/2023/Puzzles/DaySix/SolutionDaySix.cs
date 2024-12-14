#region "Usings"

using AdventOfCode.Core.RegularExpressions;

#endregion

namespace AdventOfCode.Riddles._2023.Puzzles.DaySix;

[AdventModule("Day Six")]
internal sealed class SolutionDaySix : SolutionBase
{
    #region "Constructor"

    public SolutionDaySix() : base("2023", "06", true)
    {
    }

    #endregion

    #region "Methods"

    private static long[] ParseInput(string input) =>
        (from l in RegExCollection.ExtractNumbersRegex().Matches(input)
         select long.Parse(l.Value))
        .ToArray();

    public override async Task RunAsync()
    {
        PuzzleOneResult(await SolveAsync());
        PuzzleTwoResult(await SolveAsync(true));
    }

    private async Task<long> SolveAsync(bool isPartTwo = false)
    {
        var data = isPartTwo
            ? (await InternalReadAllTextAsync()).Replace(" ", "").SplitByNewLine()
            : await InternalReadAllLinesAsync();

        var times = ParseInput(data[0]);
        var records = ParseInput(data[1]);

        var result = 1L;
        for (var i = 0; i < times.Length; i++)
        {
            result *= WinningMoves(times[i], records[i]);
        }

        return result;
    }

    private static (double, double) SolveEquation(long a, long b, long c)
    {
        var d = Math.Sqrt(b * b - 4 * a * c);
        var x1 = (-b + d) / (2 * a);
        var x2 = (-b - d) / (2 * a);
        return (Math.Min(x1, x2), Math.Max(x1, x2));
    }

    private static long WinningMoves(long time, long record)
    {
        var (x1, x2) = SolveEquation(-1, time, -record);

        var maxX = Math.Ceiling(x2) - 1;
        var minX = Math.Floor(x1) + 1;

        return (long)(maxX - minX + 1);
    }

    #endregion
}
