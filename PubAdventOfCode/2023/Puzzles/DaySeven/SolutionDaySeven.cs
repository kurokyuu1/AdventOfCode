#region "Usings"

using AdventOfCode._2023.Models.DaySeven;
using AdventOfCode.Core.Enumeration;

#endregion

namespace AdventOfCode._2023.Puzzles.DaySeven;

[AdventModule("Day seven")]
public sealed class SolutionDaySeven : SolutionBase
{
    #region "Constructor"

    public SolutionDaySeven() : base("2023", "07", true)
    {
    }

    #endregion

    #region "Methods"

    private static Hand ParseHand(string hand, bool isPartTwo = false)
    {
        var cards = hand.Split(' ');
        var bid = int.Parse(cards[^1]);
        var h = cards[..^1][0];
        return isPartTwo
            ? new(h.Replace('J', h[0]), bid, isPartTwo)
            : new(h, bid, isPartTwo);
    }

    private static int PlayGame(List<Hand> hands)
    {
        hands.Sort();
        var i = 0;

        return hands.Sum(hand => hand.Bid * ++i);
    }

    public override async Task RunAsync()
    {
        var data = await InternalReadAllLinesAsync(ReadingMode.TestInput);
        var hands = data.Select(x => ParseHand(x)).ToList();
        var hand2 = data.Select(x => ParseHand(x, true)).ToList();

        PuzzleOneResult($"Total Winnings: {PlayGame(hands)}");
        PuzzleTwoResult($"Total Winnings: {PlayGame(hand2)}");
    }

    #endregion
}
