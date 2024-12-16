namespace AdventOfCode.Riddles._2024.Puzzles.DayOne;

[AdventModule("Day One")]
public sealed class SolutionDayOne : SolutionBase
{
    #region "Constants"

    private const string NumberSeparator = "   ";

    #endregion

    #region "Properties"

    private string[] Lines { get; set; } = [];

    #endregion

    #region "Constructor"

    public SolutionDayOne() : base("2024", "01", true)
    {
    }

    #endregion

    #region "Methods"

    private Task<(List<int> LeftNumbers, List<int> RightNumbers)> BuildListAsync()
    {
        if (Lines.Length == 0)
        {
            LogToConsole("String is empty!");
            return Task.FromResult((new List<int>(), new List<int>()));
        }

        var leftNumbers = new List<int>();
        var rightNumbers = new List<int>();

        foreach (var line in Lines)
        {
            var numbers = line.Split(NumberSeparator);
            if (numbers.Length < 2 || !int.TryParse(numbers[0], out var left) ||
                !int.TryParse(numbers[1], out var right))
            {
                LogToConsole($"Invalid line format: {line}");
                continue;
            }

            leftNumbers.Add(left);
            rightNumbers.Add(right);
        }

        return Task.FromResult((leftNumbers, rightNumbers));
    }

    private async Task<int> PuzzleOneAsync()
    {
        var (leftNumbers, rightNumbers) = await BuildListAsync();

        leftNumbers.SortDescending();
        rightNumbers.SortDescending();

        return leftNumbers.Zip(rightNumbers, (left, right) => Math.Abs(right - left)).Sum();
    }

    private async Task<int> PuzzleTwoAsync()
    {
        var (leftNumbers, rightNumbers) = await BuildListAsync();
        return (from leftNumber in leftNumbers
                let count = rightNumbers.Count(x => x == leftNumber)
                select leftNumber * count).Sum();
    }

    public override async Task RunAsync()
    {
        Lines = await InternalReadAllLinesAsync();
        var result = await PuzzleOneAsync();
        SetPuzzleOneResult(result);
        //PuzzleTwo($"Sum of all numbers is: {result}");

        var result2 = await PuzzleTwoAsync();
        SetPuzzleTwoResult(result2);
        //PuzzleTwo($"Sum of all numbers is: {result2}");

        //return (result, result2);
    }

    #endregion
}
