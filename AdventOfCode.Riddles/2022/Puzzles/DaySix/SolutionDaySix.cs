#region "Usings"

using AdventOfCode.Riddles._2022.Models.DaySix;

#endregion

namespace AdventOfCode.Riddles._2022.Puzzles.DaySix;

[AdventModule("Day Six")]
internal sealed class SolutionDaySix : SolutionBase
{
    #region "Constructor"

    public SolutionDaySix() : base("2022", "06", true)
    {
    }

    #endregion

    #region "Methods"

    private static int GetMarker(string input, int count) =>
        input
            .Select((c, i) => new CharacterItem(c, i + 1))
            .Window(count)
            .FirstOrDefault(x => x.DistinctBy(y => y.Character).Count() == count)!
            .Last().Index;

    public override async Task RunAsync()
    {
        var input = await InternalReadAllTextAsync();
        PuzzleOneResult(GetMarker(input, 4));
        PuzzleTwoResult(GetMarker(input, 14));
    }

    #endregion
}
