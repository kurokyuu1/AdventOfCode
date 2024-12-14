#region "Usings"

using System.Text.RegularExpressions;
using AdventOfCode.Core.RegularExpressions;
using AdventOfCode.Riddles._2024.RegularExpressions;

#endregion

namespace AdventOfCode.Riddles._2024.Puzzles.DayThree;

[AdventModule("Day Three")]
public sealed class SolutionDayThree : SolutionBase
{
    #region "Variables"

    private string[] _lines = [];

    #endregion

    #region "Constructor"

    public SolutionDayThree() : base("2024", "03", true)
    {
    }

    #endregion

    #region "Methods"

    public override async Task<(object Result1, object Result2)> RunAsync()
    {
        _lines = await InternalReadAllLinesAsync();
        //_lines = await InternalReadAllLinesAsync(Mode);

        // you can enable this back if you want to test the solution
        //if (Mode == ReadingMode.TestInput)
        //{
        //    SolveTest();
        //}
        //else
        //{
        return SolveRealInput();
        //}
    }

    private static int SolvePuzzleOne(Span<string> lines)
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

            totalSum += sum;
        }

        PuzzleOneResult(totalSum);

        return totalSum;
    }

    private static int SolvePuzzleTwo(string input)
    {
        // here it is important to have the full string, not only the lines
        // because otherwise the regex will not work because the start and end of the don't() and do()s are not clear/found.
        var matches = RegexCollection.ExtractDontDo().Matches(input);
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

        PuzzleTwoResult(sum);
        return sum;
    }

    //private void SolveTest()
    //{
    //    // test is special because it has two different lines for each puzzle
    //    // so line one is for puzzle 1 and line two is for puzzle 2
    //    SolvePuzzleOne(_lines[..1].ToArray());
    //    SolvePuzzleTwo(string.Join(Environment.NewLine, _lines[1..]));
    //}

    private (int Result1, int Result2) SolveRealInput()
    {
        //SolvePuzzleOne(_lines);

        //// this is developed on a windows machine, so the new line is \r\n, if you are on a different machine or have different line endings, you need to adjust this according to your file input
        //SolvePuzzleTwo(string.Join(Environment.NewLine, _lines));

        return (SolvePuzzleOne(_lines), SolvePuzzleTwo(string.Join(Environment.NewLine, _lines)));
    }

    #endregion

    //private const ReadingMode Mode = ReadingMode.Input;
}
