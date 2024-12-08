namespace AdventOfCode._2022.Puzzles.DayThree;

[AdventModule("Day Three")]
internal sealed class SolutionDayThree : SolutionBase
{
    #region "Constructor"

    public SolutionDayThree() : base("2022", "03", true)
    {
    }

    #endregion

    #region "Methods"

    private static int CalculateScore(char c) =>
        c < 'a' ? c - 38 : c - 96;

    private static int GetChunkedPriority(IReadOnlyList<string> lines)
    {
        var l1 = lines[0];
        var l2 = lines[1];
        var l3 = lines[2];

        var badge = l1.FirstOrDefault(x => l2.Contains(x) && l3.Contains(x));

        return CalculateScore(badge);
    }

    private static int GetPriority(string line)
    {
        var firstHalfLength = line.Length / 2;
        var l1 = line[..firstHalfLength];
        var l2 = line[firstHalfLength..];
        var res = l1.FirstOrDefault(l2.Contains);

        return CalculateScore(res);
    }

    public override async Task RunAsync()
    {
        var input = await InternalReadAllLinesAsync();

        var prioritySum = input.Sum(GetPriority);
        var chunks = input.Chunk(3).Select(GetChunkedPriority).Sum();

        PuzzleOneResult($"Sum is: {prioritySum}");
        PuzzleTwoResult($"Sum of Badge is: {chunks}");
    }

    #endregion
}
