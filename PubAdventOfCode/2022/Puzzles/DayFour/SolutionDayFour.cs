#region "Usings"

using AdventOfCode._2022.Models.DayFour;

#endregion

namespace AdventOfCode._2022.Puzzles.DayFour;

[AdventModule("Day Four")]
internal sealed class SolutionDayFour : SolutionBase
{
    #region "Constants"

    private static readonly Dictionary<CountingMode, Func<ValueRanges, bool>> CountingMethods = new()
    {
        { CountingMode.Duplicates, IsDuplicate },
        { CountingMode.Overlap, IsOverlap },
    };

    #endregion

    #region "Constructor"

    public SolutionDayFour() : base("2022", "04", true)
    {
    }

    #endregion

    #region "Methods"

    private static int CountOccurrences(IEnumerable<string> input, CountingMode mode) =>
        input.Select(TransformString)
            .Select(x => new ValueRanges(x))
            .Where(CountingMethods[mode])
            .Select(t => t.Left).Count();

    private static bool IsDuplicate(ValueRanges v) =>
        (v.Right[0] >= v.Left[0] && v.Right[1] <= v.Left[1]) ||
        (v.Left[0] >= v.Right[0] && v.Left[1] <= v.Right[1]);

    private static bool IsOverlap(ValueRanges v) =>
        (v.Right[0] >= v.Left[0] && v.Right[0] <= v.Left[1]) ||
        (v.Left[0] >= v.Right[0] && v.Left[0] <= v.Right[1]);

    public override async Task RunAsync()
    {
        var input = await InternalReadAllLinesAsync();

        PuzzleOneResult($"Counted {CountOccurrences(input, CountingMode.Duplicates)} duplicates.");
        PuzzleTwoResult($"Counted {CountOccurrences(input, CountingMode.Overlap)} overlaps.");
    }

    private static int[][] TransformString(string line) =>
        line.Split(',').Select(token => token.Split("-").Select(int.Parse)).Select(s => s.ToArray()).ToArray();

    #endregion
}
