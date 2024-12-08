#region "Usings"

using System.Text.RegularExpressions;
using AdventOfCode._2024.RegularExpressions;
using AdventOfCode.Core.Enumeration;
using AdventOfCode.Core.RegularExpressions;

#endregion

namespace AdventOfCode._2024.Puzzles.DayThree;

[AdventModule("Day Three")]
public sealed class SolutionDayThree : SolutionBase
{
    #region "Constants"

    private const ReadingMode Mode = ReadingMode.Input;

    #endregion

    #region "Variables"

    private string[] _lines = [];

    #endregion

    #region "Constructor"

    public SolutionDayThree() : base("2024", "03", false)
    {
    }

    #endregion

    #region "Methods"

    public override async Task RunAsync()
    {
        _lines = await InternalReadAllLinesAsync();

        SolvePuzzleOne(Mode == ReadingMode.TestInput
            ? _lines[..1].ToArray()
            : _lines); // produces correct result for both test and input
        SolvePuzzleTwo(Mode == ReadingMode.TestInput
            ? _lines[1..].ToArray()
            : _lines); // produces correct result for only test, input seems to be off
    }

    private void SolvePuzzleOne(Span<string> lines)
    {
        var totalSum = 0;

        foreach (var line in lines)
        {
            var matches = RegexCollection.ExtractMul().Matches(line);
            var sum = 0;

            foreach (Match match in matches)
            {
                var values = RegExCollection.ExtractNumbersRegex().Matches(match.Value);
                if (values.Count != 2)
                {
                    throw new InvalidOperationException("Invalid number of values");
                }

                var firstValue = int.Parse(values[0].Value);
                var secondValue = int.Parse(values[1].Value);

                sum += firstValue * secondValue;
            }

            LogToConsole($"Sum of {line} is: {sum}");
            totalSum += sum;
        }

        PuzzleOneResult(totalSum);
    }

    private void SolvePuzzleTwo(Span<string> lines)
    {
        var totalSum = 0;

        foreach (var line in lines)
        {
            var matches = RegexCollection.ExtractDontDo().Matches(line);
            var sum = 0;
            var isWithinDo = true;
            foreach (Match match in matches)
            {
                if (match.Groups["dont"].Success)
                {
                    isWithinDo = false;
                }
                else if (match.Groups["do"].Success)
                {
                    isWithinDo = true;
                }
                else if (match.Groups["mul"].Success && isWithinDo)
                {
                    var values = RegExCollection.ExtractNumbersRegex().Matches(match.Value);
                    if (values.Count != 2)
                    {
                        throw new InvalidOperationException("Invalid number of values");
                    }

                    var firstValue = int.Parse(values[0].Value);
                    var secondValue = int.Parse(values[1].Value);
                    sum += firstValue * secondValue;
                }
            }

            LogToConsole($"Sum of {line} is: {sum}");
            totalSum += sum;
        }

        PuzzleTwoResult(totalSum);
    }

    #endregion
}
