namespace AdventOfCode._2022.Models.DayThirteen;

internal sealed class ComparePair : IComparable<CompareToken>
{
    #region "Properties"

    public int Index { get; set; }

    private CompareToken Left { get; }
    private CompareToken Right { get; }

    #endregion

    #region "Constructor"

    public ComparePair(IReadOnlyList<string> pair, int index)
    {
        if (pair.Count != 2)
        {
            throw new ArgumentException(nameof(pair), "The passed pair array must exactly 2 elements.");
        }

        Left = new(pair[0]);
        Right = new(pair[1]);
        Index = index;
    }

    #endregion

    #region "Interface Methods"

    public int CompareTo(CompareToken? other) => Left.CompareTo(other!);

    #endregion

    #region "Methods"

    public int Compare() => Left.CompareTo(Right);

    #endregion
}
