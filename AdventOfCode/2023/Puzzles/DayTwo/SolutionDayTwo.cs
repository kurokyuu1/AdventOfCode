#region "Usings"

using System.Text.RegularExpressions;

#endregion

namespace AdventOfCode._2023.Puzzles.DayTwo;

[AdventModule("Day Two")]
internal sealed partial class SolutionDayTwo : SolutionBase
{
    #region "Constructor"

    public SolutionDayTwo() : base("2023", "02", true)
    {
    }

    #endregion

    #region "Methods"

    private static int BagMaxValue(string input, Regex regex)
    {
        var matches = regex.Matches(input);
        return matches.Count == 0
            ? 0
            : matches.Select(x => int.Parse(x.Groups[1].Value)).Max();
    }

    private static int CalculateGamesPossible(IEnumerable<string> data, int maxRed, int maxBlue, int maxGreen) =>
        (from s in data
         let gameSet = ParseGameSet(s)
         where IsWithinRange(maxRed, maxBlue, maxGreen, gameSet)
         select gameSet.GameId).Sum();

    private static int CalculatePower(IEnumerable<string> data) =>
        (from s in data
         let gameSet = ParseGameSet(s)
         select gameSet.Power
        ).Sum();

    [GeneratedRegex(@"(\d+) blue", RegexOptions.Compiled)]
    private static partial Regex ExtractBlueItems();

    [GeneratedRegex(@"Game (\d+)", RegexOptions.Compiled)]
    private static partial Regex ExtractGame();

    [GeneratedRegex(@"(\d+) green", RegexOptions.Compiled)]
    private static partial Regex ExtractGreenItems();

    [GeneratedRegex(@"(\d+) red", RegexOptions.Compiled)]
    private static partial Regex ExtractRedItems();

    private static bool IsWithinRange(int maxRed, int maxBlue, int maxGreen, GameSet gameSet) =>
        gameSet.Red <= maxRed
        && gameSet.Blue <= maxBlue
        && gameSet.Green <= maxGreen;

    private static GameSet ParseGameSet(string input)
    {
        var game = ExtractGame().Matches(input).Select(x => int.Parse(x.Groups[1].Value)).First();
        var red = BagMaxValue(input, ExtractRedItems());
        var blue = BagMaxValue(input, ExtractBlueItems());
        var green = BagMaxValue(input, ExtractGreenItems());

        return new(game, red, blue, green);
    }

    public override async Task RunAsync()
    {
        var data = await InternalReadAllLinesAsync();

        if (data.Length == 0)
        {
            LogToConsole("String is empty!");
            return;
        }

        PuzzleOneResult($"Possible games by summing up game ids is: {CalculateGamesPossible(data, 12, 14, 13)}");
        PuzzleTwoResult($"Sum of Game-Set Power: {CalculatePower(data)}");
    }

    #endregion
}

internal sealed record GameSet(int GameId, int Red, int Blue, int Green)
{
    #region "Properties"

    public int Power => Red * Blue * Green;

    #endregion
}
