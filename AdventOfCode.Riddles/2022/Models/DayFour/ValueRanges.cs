namespace AdventOfCode.Riddles._2022.Models.DayFour;

internal sealed record ValueRanges(IReadOnlyList<int> Left, IReadOnlyList<int> Right)
{
    #region "Constructor"

    internal ValueRanges(IReadOnlyList<IReadOnlyList<int>> array) : this(array[0], array[1])
    {
    }

    #endregion
}
