namespace AdventOfCode._2024.Puzzles.DayTwo;

[AdventModule("Day Two")]
public sealed class SolutionDayTwo : SolutionBase
{
    #region "Constants"

    private const int MaxDifference = 3;
    private const string SpaceSeparator = " ";

    #endregion

    #region "Constructor"

    public SolutionDayTwo() : base("2024", "02", false)
    {
    }

    #endregion

    #region "Methods"

    private async Task<int[][]> BuildReportAsync()
    {
        var lines = await InternalReadAllLinesAsync();
        //var lines = await InternalReadAllLinesAsync(ReadingMode.TestInput);
        var reports = lines.Select(x => x.Split(SpaceSeparator).Select(int.Parse).ToArray()).ToArray();

        return reports;
    }

    private static bool IsSafe(Span<int> report)
    {
        if (report.Length <= 2)
        {
            return true;
        }

        // this way we determine if the list is increasing or decreasing
        var isIncreasing = report[0] < report[1];
        for (var j = 0; j < report.Length - 1; j++)
        {
            // rules:
            // safe if numbers in list all increasing or decreasing
            // the increase/decrease must be at least 1 and at max 3
            if ((isIncreasing && report[j] > report[j + 1]) || (!isIncreasing && report[j] < report[j + 1]))
            {
                return false;
            }

            var diff = Math.Abs(report[j + 1] - report[j]);
            if (diff is 0 or > MaxDifference)
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsSafeWithTolerance(Span<int> report)
    {
        if (report.Length <= 2)
        {
            return IsSafe(report);
        }

        for (var i = 0; i < report.Length; i++)
        {
            var sliced = i == 0
                ? report[1..] // remove first element
                : i == report.Length - 1
                    ? report[..^1] // remove last element
                    : report[..i].ToArray().Concat(report[(i + 1)..].ToArray())
                        .ToArray(); // remove element in the middle

            if (IsSafe(sliced))
            {
                return true;
            }
        }

        return false;
    }

    public override async Task RunAsync()
    {
        await SolvePuzzleAsync();
        await SolvePuzzleAsync(true);
    }

    private async Task SolvePuzzleAsync(bool isPart2 = false)
    {
        var reports = await BuildReportAsync();
        var safeReports = 0;
        // with this we can switch between the two validation methods and use this for the two parts.
        var validationMethod = (Func<Span<int>, bool>)(isPart2 ? IsSafeWithTolerance : IsSafe);

        foreach (var report in reports)
        {
            if (validationMethod(report))
            {
                safeReports++;
                LogToConsole($"[Safe] {string.Join(SpaceSeparator, report)}");
            }
            else
            {
                LogToConsole($"[Unsafe] {string.Join(SpaceSeparator, report)}");
            }
        }

        if (!isPart2)
        {
            PuzzleOneResult(safeReports);
        }
        else
        {
            PuzzleTwoResult(safeReports);
        }
    }

    #endregion
}
