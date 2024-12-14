#region "Usings"

using AdventOfCode.Core.RegularExpressions;

#endregion

namespace AdventOfCode.Riddles._2023.Puzzles.DayOne;

[AdventModule("Day One")]
internal sealed class SolutionDayOne : SolutionBase
{
    #region "Constructor"

    public SolutionDayOne() : base("2023", "01", true)
    {
    }

    #endregion

    #region "Methods"

    public override async Task RunAsync()
    {
        var data = await InternalReadAllLinesAsync();
        //var data = await InternalReadAllLinesAsync(ReadingMode.TestInput);

        if (data.Length == 0)
        {
            LogToConsole("String is empty!");
            return;
        }

        var sum = (from s in data
                   select RegExCollection.ExtractNumberRegex().Matches(s)
                   into numbers
                   where numbers.Count > 0
                   let first = GetNumber(numbers.First().Value)
                   let last = GetNumber(numbers.Last().Value)
                   select first * 10 + last
            ).Sum();

        var sum2 = (from s in data
                    let first = RegExCollection.ExtractNumbersFromWordsRegex().Match(s)
                    let last = RegExCollection.ExtractNumbersFromWordsReversed().Match(s)
                    select GetNumber(first.Value) * 10 + GetNumber(last.Value)
            ).Sum();
        PuzzleOneResult($"Sum of all numbers is: {sum}");
        PuzzleTwoResult($"Sum of all numbers is: {sum2}");
        return;

        static long GetNumber(string s) =>
            s switch
            {
                "one" => 1,
                "two" => 2,
                "three" => 3,
                "four" => 4,
                "five" => 5,
                "six" => 6,
                "seven" => 7,
                "eight" => 8,
                "nine" => 9,
                var _ => long.Parse(s),
            };
    }

    #endregion
}
