#region "Usings"

using AdventOfCode.Core.Enumeration;

#endregion

namespace AdventOfCode._2022.Puzzles.DayNine;

[AdventModule("Day Nine")]
internal sealed class SolutionDayNine : SolutionBase
{
    #region "Variables"

    private readonly HashSet<RopePosition> _visited = new();

    private RopePosition _head = new(0, 0);
    private RopePosition _tail;

    #endregion

    #region "Constructor"

    public SolutionDayNine() : base("2022", "09", true)
    {
        _tail = _head;
        _visited.Add(_tail);
    }

    #endregion

    #region "Methods"

    public override async Task RunAsync()
    {
        await SimulateAsync();
        PuzzleOneResult($"Tail visited {_visited.Count} positions!");
    }

    private async Task SimulateAsync()
    {
        var input = (await InternalReadAllLinesAsync(ReadingMode.TestInput))
            .Select(x => new { Direction = x[0], Distance = int.Parse(x[2..]) }).ToArray();

        foreach (var chr in input)
        {
            for (var i = 0; i < chr.Distance; i++)
            {
                _head = _head.Move(chr.Direction);

                if (!_head.IsAdjacent(_tail))
                {
                    _tail = _head.IsAligned(_tail) ? _tail.Move(chr.Direction) : _tail.MoveDiagonal(_head);
                }

                _visited.Add(_tail);
            }
        }
    }

    #endregion

    #region "Nested"

    internal readonly record struct RopePosition
    {
        #region "Variables"

        private readonly int X;
        private readonly int Y;

        #endregion

        #region "Constructor"

        public RopePosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region "Methods"

        private int DistanceTo(RopePosition other) => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

        public bool IsAdjacent(RopePosition other) => DistanceTo(other) <= 1;
        public bool IsAligned(RopePosition other) => X == other.X || Y == other.Y;

        public RopePosition Move(char direction) =>
            direction switch
            {
                'U' => new(X, Y - 1),
                'R' => new(X + 1, Y),
                'D' => new(X, Y + 1),
                'L' => new(X - 1, Y),
                var _ => throw new ArgumentException($"Unknown direction '{direction}'.", nameof(direction)),
            };

        public RopePosition MoveDiagonal(RopePosition other)
        {
            var dx = Math.Sign(other.X - X);
            var dy = Math.Sign(other.Y - Y);

            return new(dx, dy);
        }

        #endregion
    }

    #endregion
}
