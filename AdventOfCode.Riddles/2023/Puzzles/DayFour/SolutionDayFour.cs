#region "Usings"

using AdventOfCode.Core.RegularExpressions;

#endregion

namespace AdventOfCode.Riddles._2023.Puzzles.DayFour;

[AdventModule("Day Four")]
internal sealed class SolutionDayFour : SolutionBase
{
    #region "Constructor"

    public SolutionDayFour() : base("2023", "04", true)
    {
    }

    #endregion

    #region "Methods"

    private static Card ParseCard(string line)
    {
        var parts = line.Split(':', '|');
        var left = from x in RegExCollection.ExtractNumbersRegex().Matches(parts[1]) select x.Value;
        var right = from x in RegExCollection.ExtractNumbersRegex().Matches(parts[2]) select x.Value;

        return new(left.Intersect(right).Count());
    }

    public override async Task RunAsync()
    {
        var data = await InternalReadAllLinesAsync();

        var puzzle1Sum = (from x in data
                          let card = ParseCard(x)
                          where card.Matches > 0
                          select Math.Pow(2, card.Matches - 1))
            .Sum();

        var cards = data.Select(ParseCard).ToArray();
        var cardCounts = cards.Select(_ => 1).ToArray();

        for (var i = 0; i < cards.Length; i++)
        {
            var (card, count) = (cards[i], cardCounts[i]);
            for (var j = 0; j < card.Matches; j++)
            {
                cardCounts[i + j + 1] += count;
            }
        }

        var puzzle2Sum = cardCounts.Sum();

        SetPuzzleOneResult($"Sum: {puzzle1Sum}");
        SetPuzzleTwoResult($"Sum: {puzzle2Sum}");
    }

    #endregion
}

internal sealed record Card(int Matches);
