using AdventOfCode.Core.Enumeration;

namespace AdventOfCode.Riddles._2024.Puzzles.DayFive;

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
                let result = IsValidOrder(rule)
                where result.IsValid
                select result.FirstIndex)
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

    private (bool IsValid, int FirstIndex, int SecondIndex) IsValidOrder(int[] rule)
    {
        var (firstIdx, secondIdx) = (Array.IndexOf(Manual, rule[0]), Array.IndexOf(Manual, rule[1]));

        if (firstIdx == -1 && secondIdx == -1)
        {
            return (false, 0, 0);
        }
        return firstIdx != -1 && secondIdx == -1 || firstIdx == -1 && secondIdx != -1 || firstIdx < secondIdx
            ? (true, firstIdx, secondIdx)
            : (false, firstIdx, secondIdx);
    }

    //public int GetMiddleValueAndFixOrder()
    //{
    //    var alLCorrected = false;
    //    foreach (var rule in Rules)
    //    {
    //        alLCorrected = false;
    //        var (isValid, idx1, idx2) = IsValidOrder(rule);

    //        if (isValid || idx1 == 0 || idx2 == 0)
    //        {
    //            continue;
    //        }

    //        (Manual[idx2], Manual[idx1]) = (Manual[idx1], Manual[idx2]);
    //        alLCorrected = true;
    //    }

    //    //if (!alLCorrected)
    //    //{
    //    //    return 0;
    //    //}

    //    var middleIndex = Manual.Length / 2;

    //    return Manual[middleIndex];
    //}
}

[AdventModule("Day Five")]
public sealed class SolutionDayFive : SolutionBase
{
    public SolutionDayFive() : base("2024", "05", true)
    {

    }

    public override async Task<(object? Result1, object? Result2)> RunAsync()
    {
        var input = await InternalReadAllTextAsync(ReadingMode.TestInput);
        var split = input.SplitByDoubleNewLine();
        var rules = split[0].SplitByNewLine().Select(x => x.Split("|").Select(int.Parse).ToArray()).ToList();
        var manuals = split[1].SplitByNewLine().Select(manual => manual.Split(",").Select(int.Parse).ToArray()).ToList();

        var pages = manuals.Select(manual => new PageOrder(rules, manual)).ToList();
        var sumOfMiddleValues = pages.Sum(x => x.GetMiddleValue());
        //var unordered = pages.Sum(x => x.GetMiddleValueAndFixOrder()); // does not work...

        PuzzleOneResult(sumOfMiddleValues);
        //PuzzleTwoResult(unordered);

        return (sumOfMiddleValues, null);
    }
}
