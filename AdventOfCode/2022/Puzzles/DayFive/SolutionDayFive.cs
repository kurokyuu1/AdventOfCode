#region "Usings"

using System.Text.RegularExpressions;
using AdventOfCode._2022.Models.DayFive;

#endregion

namespace AdventOfCode._2022.Puzzles.DayFive;

[AdventModule("Day Five")]
internal sealed partial class SolutionDayFive : SolutionBase
{
    #region "Constructor"

    public SolutionDayFive() : base("2022", "05", true)
    {
    }

    #endregion

    #region "Methods"

    private static string CrateMover(Day5Modes mover, StackData data)
    {
        var stacks = new IEnumerable<char>[data.Stacks.Length];
        data.Stacks.CopyTo(stacks, 0);

        foreach (var item in data.Commands.AsSpan())
        {
            switch (mover)
            {
                case Day5Modes.Puzzle1:
                    for (var i = 0; i < item.Quantity; i++)
                    {
                        MoveItems(item, stacks, mover);
                    }

                    break;
                case Day5Modes.Puzzle2:
                    MoveItems(item, stacks, mover);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mover), mover, null);
            }
        }

        return new(stacks.Select(s => s.Last()).ToArray());
    }

    [GeneratedRegex(@"move (\d+) from (\d+) to (\d+)", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
    private static partial Regex ExtractOrderCommands();

    private static void MoveItems(OrderCommand cmd, IList<IEnumerable<char>> source, Day5Modes mode = Day5Modes.Puzzle1)
    {
        var qyt = mode == Day5Modes.Puzzle1 ? 1 : cmd.Quantity;
        source[cmd.To] = source[cmd.To].Concat(source[cmd.From].TakeLast(qyt)).ToArray();
        source[cmd.From] = source[cmd.From].SkipLast(qyt).ToArray();
    }

    private static StackData Parse(string input)
    {
        var split = input.Split($"{Environment.NewLine}{Environment.NewLine}");

        var commands = split[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();
        var data = split[0]
            .SplitByNewLine()
            .Select(line => line.Chunk(4).Select(x => x[1]))
            .SelectMany(inner => inner.Select((item, index) => new { item, index }))
            .GroupBy(i => i.index, i => i.item)
            .Select(g => g.Reverse().Where(c => c != ' ').Skip(1))
            .ToArray();

        var commandList = commands
            .Select(item => ExtractOrderCommands().Match(item))
            .Select(matches => new OrderCommand(int.Parse(matches.Groups[1].Value),
                int.Parse(matches.Groups[2].Value) - 1, int.Parse(matches.Groups[3].Value) - 1))
            .ToArray();

        return new(data, commandList);
    }

    public override async Task RunAsync()
    {
        var input = await InternalReadAllTextAsync();
        var parsedData = Parse(input);

        PuzzleOneResult($"{CrateMover(Day5Modes.Puzzle1, parsedData)}");
        PuzzleTwoResult($"{CrateMover(Day5Modes.Puzzle2, parsedData)}");
    }

    #endregion
}
