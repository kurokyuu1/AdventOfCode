#region "Usings"

using System.Text.RegularExpressions;
using AdventOfCode.Core.RegularExpressions;

#endregion

namespace AdventOfCode.Riddles._2023.Puzzles.DayThree;

[AdventModule("Day Three")]
internal sealed class SolutionDayThree : SolutionBase
{
    #region "Constructor"

    public SolutionDayThree() : base("2023", "03", true)
    {
    }

    #endregion

    #region "Methods"

    private static bool IsAdjacent(EnginePart part1, EnginePart part2) =>
        Math.Abs(part2.Row - part1.Row) <= 1
        && part1.Column <= part2.Column + part2.Text.Length
        && part2.Column <= part1.Column + part1.Text.Length;

    private static EnginePart[] Parse(string[] data, Regex regex)
        => (from row in Enumerable.Range(0, data.Length)
            from match in regex.Matches(data[row])
            select new EnginePart(match.Value, row, match.Index)).ToArray();

    public override async Task RunAsync()
    {
        var data = await InternalReadAllLinesAsync();
        var numbers = Parse(data, RegExCollection.ExtractNumbersRegex());
        var symbols = Parse(data, RegExCollection.ExtractSymbols());
        var gears = Parse(data, RegExCollection.ExtractGears());

        var sum = (from number in numbers
                   from symbol in symbols
                   where IsAdjacent(number, symbol)
                   select number.Value)
            .Sum();

        var sum2 = (
                from gear in gears
                let neighbours = from number in numbers
                                 where IsAdjacent(number, gear)
                                 select number.Value
                where neighbours.Count() == 2
                select neighbours.First() * neighbours.Last())
            .Sum();

        PuzzleOneResult($"Sum of all numbers is: {sum}");
        PuzzleTwoResult($"Sum of all numbers is: {sum2}");
    }

    #endregion
}

internal sealed record EnginePart(string Text, int Row, int Column)
{
    #region "Properties"

    public int Value => int.Parse(Text);

    #endregion
}
