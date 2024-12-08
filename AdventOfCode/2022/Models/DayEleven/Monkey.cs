namespace AdventOfCode._2022.Models.DayEleven;

internal sealed class Monkey
{
    #region "Properties"

    public int Inspections { get; set; }

    public string Name { get; init; } = default!;
    public List<long> StartingItems { get; set; } = [];
    public int Test { get; init; }
    public int TestIfFalse { get; init; }
    public int TestIfTrue { get; init; }
    public WorryOperation WorryOperation { get; init; } = default!;

    #endregion
}
