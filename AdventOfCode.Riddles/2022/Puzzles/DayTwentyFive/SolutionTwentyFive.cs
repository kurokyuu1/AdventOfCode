#region "Usings"

using System.Text;

#endregion

namespace AdventOfCode.Riddles._2022.Puzzles.DayTwentyFive;

[AdventModule("Day Twenty-Five")]
internal sealed class SolutionTwentyFive : SolutionBase
{
    #region "Constructor"

    public SolutionTwentyFive()
        : base("2022", "25", true)
    {
    }

    #endregion

    #region "Methods"

    public override async Task RunAsync()
    {
        var input = await InternalReadAllLinesAsync();
        SolutionOne(input);
    }

    private static long SnafuDecode(string input)
    {
        return input == null
            ? throw new ArgumentNullException(nameof(input))
            : input.Select((_, i) => input.Length - 1 - i)
                .Select(idx => input[idx] switch
                {
                    '=' => -2,
                    '-' => -1,
                    var _ => input[idx] - '0',
                })
                .Select((dec, i) => (long)Math.Pow(5, i) * dec)
                .Sum();
    }

    private static string SnafuEncode(long num)
    {
        var result = new StringBuilder();

        while (num > 0)
        {
            var x = num % 5;

            switch (x)
            {
                case 3:
                    x = -2;
                    result.Append('=');
                    break;
                case 4:
                    x = -1;
                    result.Append('-');
                    break;
                default:
                    result.Append(x);
                    break;
            }

            num = (num - x) / 5;
        }

        return result.ToString();
    }

    private void SolutionOne(IEnumerable<string> input)
    {
        SetPuzzleOneResult(SnafuEncode(input.Sum(SnafuDecode)));
    }

    #endregion
}
