namespace AdventOfCode._2024.Puzzles.DayFour;

[AdventModule("Day Four")]
public sealed class SolutionDayFour : SolutionBase
{
    #region "Constants"

    private const string SearchWord = "XMAS";
    private const int SearchWordLength = 4;

    private static readonly int[,] SearchDirections = new[,]
    {
        { 0, 1 }, // horizontal right,
        { 0, -1 }, // horizontal left,
        { 1, 0 }, // vertical down,
        { -1, 0 }, // vertical up,
        { 1, 1 }, // diagonal down right,
        { 1, -1 }, // diagonal down left,
        { -1, 1 }, // diagonal up right,
        { -1, -1 }, // diagonal up left,
    };

    #endregion

    #region "Constructor"

    public SolutionDayFour() : base("2024", "04", true)
    {
    }

    #endregion

    #region "Methods"

    private static int CountOccurrences(char[,] grid)
    {
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);
        var count = 0;

        for (var x = 0; x < rows; x++)
        {
            for (var y = 0; y < cols; y++)
            {
                for (var directionIndex = 0; directionIndex < SearchDirections.GetLength(0); directionIndex++)
                {
                    if (Search(grid, x, y, directionIndex))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    public override async Task RunAsync()
    {
        await SolvePuzzleOne();
    }

    private static bool Search(char[,] grid, int x, int y, int directionIndex)
    {
        for (var i = 0; i < SearchWordLength; i++)
        {
            var newX = x + SearchDirections[directionIndex, 0] * i;
            var newY = y + SearchDirections[directionIndex, 1] * i;

            // check if the new coordinates are out of bounds
            if (newX < 0 || newX >= grid.GetLength(0) || newY < 0 || newY >= grid.GetLength(1))
            {
                return false;
            }

            if (grid[newX, newY] != SearchWord[i])
            {
                return false;
            }
        }

        return true;
    }

    private async Task SolvePuzzleOne()
    {
        var lines = await InternalReadAllLinesAsync();
        var grid = new char[lines.Length, lines[0].Length];

        // build the grid
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                grid[i, j] = lines[i][j];
            }
        }

        var count = CountOccurrences(grid);

        PuzzleOneResult(count);
    }

    #endregion
}
