#region "Usings"

using AdventOfCode.Riddles._2022.Models.DayThirteen;

#endregion

namespace AdventOfCode.Riddles._2022.Puzzles.DayThirteen;

[AdventModule("Day Thirteen")]
internal sealed class SolutionDayThirteen : SolutionBase
{
    #region "Constants"

    private static readonly string[] Separator = ["\r\n\r\n", "\r\n"];

    #endregion

    #region "Constructor"

    public SolutionDayThirteen()
        : base("2022", "13", true)
    {
    }

    #endregion

    #region "Methods"

    public override async Task RunAsync()
    {
        var input = await InternalReadAllTextAsync();
        SolvePuzzleOne(input);
        SolvePuzzleTwo(input);
    }

    private void SolvePuzzleOne(string input)
    {
        var sum = input
            .SplitByDoubleNewLine()
            .Select(x => x.SplitByNewLine())
            .Select((x, i) => new ComparePair(x, i))
            .Where(item => item.Compare() <= 0).Sum(item => item.Index + 1);
        SetPuzzleOneResult(sum);
    }

    private void SolvePuzzleTwo(string input)
    {
        input += "\r\n\r\n[[2]]\r\n[[6]]";
        var packages = input.Split(Separator, StringSplitOptions.RemoveEmptyEntries).Select(x => new CompareToken(x))
            .ToList();

        // these are 2, and 6
        var lastPackages = packages.TakeLast(2).ToArray();
        packages.Sort();

        var result = (packages.IndexOf(lastPackages[0]) + 1) * (packages.IndexOf(lastPackages[1]) + 1);
        SetPuzzleTwoResult(result);
    }

    #endregion
}
