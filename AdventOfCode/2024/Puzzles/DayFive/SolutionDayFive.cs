namespace AdventOfCode._2024.Puzzles.DayFive;

internal sealed record PageOrder(List<int[]> Rules, int[] Manual)
{
    public int GetMiddleValue()
    {
        var filteredRules = Rules.Where(rule => rule.Any(page => Manual.Contains(page))).ToArray();
        if (filteredRules.Length == 0)
        {
            return 0;
        }

        var allCorrect = false;
        var correctUpdates = (
                from rule in filteredRules 
                let firstIdx = Array.IndexOf(Manual, rule[0]) 
                let secondIdx = Array.IndexOf(Manual, rule[1]) 
                where firstIdx != -1 || secondIdx != -1 
                where firstIdx != -1 && secondIdx == -1 || firstIdx == -1 && secondIdx != -1 || firstIdx < secondIdx 
                select firstIdx)
            .Count();

        if (correctUpdates == filteredRules.Length)
        {
            allCorrect = true;
        }

        if (!allCorrect)
        {
            return 0;
        }

        var middleIndex = Manual.Length / 2;

        return Manual[middleIndex];
    }
}

[AdventModule("Day Five")]
public sealed class SolutionDayFive : SolutionBase
{
    public SolutionDayFive() : base("2024", "05", true)
    {

    }

    public override async Task RunAsync()
    {
        var input = await InternalReadAllTextAsync();
        var split = input.SplitByDoubleNewLine();
        var rules = split[0].SplitByNewLine().Select(x => x.Split("|").Select(int.Parse).ToArray()).ToList();
        var manuals = split[1].SplitByNewLine().Select(manual => manual.Split(",").Select(int.Parse).ToArray()).ToList();

        var sumOfMiddleValues = manuals.Select(manual => new PageOrder(rules, manual)).Sum(x => x.GetMiddleValue());

        PuzzleOneResult(sumOfMiddleValues);
    }
}
