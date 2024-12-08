namespace AdventOfCode._2022.Puzzles.DayOne;

[AdventModule("Day One")]
internal sealed class SolutionDayOne : SolutionBase
{
    #region "Constructor"

    public SolutionDayOne() : base("2022", "01", true)
    {
    }

    #endregion

    #region "Methods"

    public override async Task RunAsync()
    {
        var data = await InternalReadAllTextAsync();

        if (string.IsNullOrEmpty(data))
        {
            LogToConsole("String is empty!");
            return;
        }

        var maxCalories = data
            .SplitByDoubleNewLine()
            .Select(x => x.SplitByNewLine().Sum(int.Parse)).ToList();

        PuzzleOneResult($"Max calories are: {maxCalories.Max()}");
        PuzzleTwoResult($"Total calories of the top three is: {maxCalories.OrderByDescending(x => x).Take(3).Sum()}");
    }

    #endregion
}
