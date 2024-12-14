namespace AdventOfCode.Riddles._2023.Puzzles.DayNine;

[AdventModule("Day Nine")]
internal sealed class SolutionDayNine : SolutionBase
{
    #region "Constructor"

    public SolutionDayNine() : base("2023", "09", true)
    {
    }

    #endregion

    #region "Methods"

    private static Span<long> Difference(Span<long> numbers)
    {
        var result = new Span<long>(new long[numbers.Length - 1]);
        for (var i = 0; i < numbers.Length - 1; i++)
        {
            result[i] = numbers[i + 1] - numbers[i];
        }

        return result;
    }

    private static long ExtrapolateLeft(Span<long> numbers) =>
        numbers.Length == 0 ? 0 : numbers[0] - ExtrapolateLeft(Difference(numbers));

    private static long ExtrapolateRight(Span<long> numbers) =>
        numbers.Length == 0 ? 0 : numbers[^1] + ExtrapolateRight(Difference(numbers));

    private static Span<long> ParseNumbers(string line) =>
        line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();

    public override async Task RunAsync()
    {
        var lines = await InternalReadAllLinesAsync();

        PuzzleOneResult(lines.Select(x => ExtrapolateRight(ParseNumbers(x))).Sum());
        PuzzleTwoResult(lines.Select(x => ExtrapolateLeft(ParseNumbers(x))).Sum());
    }

    #endregion
}
