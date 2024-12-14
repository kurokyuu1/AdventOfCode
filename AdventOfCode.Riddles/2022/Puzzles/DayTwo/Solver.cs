#region "Usings"

using AdventOfCode.Riddles._2022.Models.DayTwo;

#endregion

namespace AdventOfCode.Riddles._2022.Puzzles.DayTwo;

internal static class Solver
{
    #region "Constants"

    private static readonly Dictionary<string, PlayerHand> Hands = new()
    {
        { "A", PlayerHand.Rock },
        { "B", PlayerHand.Paper },
        { "C", PlayerHand.Scissor },

        { "X", PlayerHand.Rock },
        { "Y", PlayerHand.Paper },
        { "Z", PlayerHand.Scissor },
    };

    private static readonly Dictionary<PlayerHand, int> HandScores = new()
    {
        { PlayerHand.Rock, 1 },
        { PlayerHand.Paper, 2 },
        { PlayerHand.Scissor, 3 },
    };

    private static readonly Dictionary<string, Outcome> Outcomes = new()
    {
        { "X", Outcome.Lose },
        { "Y", Outcome.Draw },
        { "Z", Outcome.Win },
    };

    private static readonly Dictionary<Outcome, int> OutcomeScores = new()
    {
        { Outcome.Win, 6 },
        { Outcome.Lose, 0 },
        { Outcome.Draw, 3 },
    };

    #endregion

    #region "Methods"

    public static int CalculateEndScore(GameHandResult handResult)
    {
        var outcome = GetResult(handResult.Player, handResult.Opponent);

        return HandScores[handResult.Player] + OutcomeScores[outcome];
    }

    public static int CalculateRiggedEndScore(GameOutcomeResult result)
    {
        var player = RigGame(result);

        return HandScores[player] + OutcomeScores[result.Outcome];
    }

    private static Outcome GetResult(PlayerHand player, PlayerHand opponent)
    {
        if (player == opponent)
        {
            return Outcome.Draw;
        }

        return player switch
        {
            PlayerHand.Rock when opponent == PlayerHand.Scissor => Outcome.Win,
            PlayerHand.Paper when opponent == PlayerHand.Rock => Outcome.Win,
            PlayerHand.Scissor when opponent == PlayerHand.Paper => Outcome.Win,
            var _ => Outcome.Lose,
        };
    }

    public static IEnumerable<GameHandResult> ParseHandsList(IEnumerable<string> lines) =>
        lines
            .Select(item => item.SplitBySpace())
            .Select(x => new GameHandResult(Hands[x[0]], Hands[x[1]]));

    public static IEnumerable<GameOutcomeResult> ParseOutcomeList(IEnumerable<string> lines) =>
        lines
            .Select(item => item.SplitBySpace())
            .Select(x => new GameOutcomeResult(Hands[x[0]], Outcomes[x[1]]));

    private static PlayerHand RigGame(GameOutcomeResult result) =>
        result.Outcome switch
        {
            Outcome.Draw => result.Hand,
            Outcome.Win => result.Hand switch
            {
                PlayerHand.Rock => PlayerHand.Paper,
                PlayerHand.Paper => PlayerHand.Scissor,
                PlayerHand.Scissor => PlayerHand.Rock,
                var _ => throw new ArgumentOutOfRangeException(nameof(result)),
            },
            var _ => result.Hand switch
            {
                PlayerHand.Rock => PlayerHand.Scissor,
                PlayerHand.Paper => PlayerHand.Rock,
                PlayerHand.Scissor => PlayerHand.Paper,
                var _ => throw new ArgumentOutOfRangeException(nameof(result)),
            },
        };

    #endregion
}
