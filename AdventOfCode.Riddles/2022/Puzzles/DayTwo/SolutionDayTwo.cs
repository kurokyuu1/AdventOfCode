namespace AdventOfCode.Riddles._2022.Puzzles.DayTwo;

[AdventModule("Day Two")]
internal sealed class SolutionDayTwo : SolutionBase
{
    #region "Constructor"

    public SolutionDayTwo() : base("2022", "02", true)
    {
    }

    #endregion

    #region "Methods"

    public override async Task RunAsync()
    {
        var data = await InternalReadAllLinesAsync();
        var parsedList = Solver.ParseHandsList(data);
        var riggedList = Solver.ParseOutcomeList(data);

        SetPuzzleOneResult(parsedList.Sum(Solver.CalculateEndScore));
        SetPuzzleTwoResult(riggedList.Sum(Solver.CalculateRiggedEndScore));
    }

    #endregion
}
