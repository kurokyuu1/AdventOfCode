#region "Usings"

using AdventOfCode.Core.RegularExpressions;

#endregion

namespace AdventOfCode.Riddles._2023.Puzzles.DayFive;

[AdventModule("Day Five")]
internal sealed class SolutionDayFive : SolutionBase
{
    #region "Constructor"

    public SolutionDayFive() : base("2023", "05", true)
    {
    }

    #endregion

    #region "Methods"

    private static Dictionary<Range, Range> ParseMap(string line) =>
        (from l in line.SplitByNewLine().Skip(1)
         let parts = ParseNumbers(l).ToArray()
         select new KeyValuePair<Range, Range>(
             new(parts[1], parts[2] + parts[1] - 1),
             new(parts[0], parts[2] + parts[0] - 1)))
        .ToDictionary();

    private static IEnumerable<long> ParseNumbers(string line) =>
        from x in RegExCollection.ExtractNumbersRegex().Matches(line)
        select long.Parse(x.Value);

    private static List<Range> Project(List<Range> ranges, Dictionary<Range, Range> map)
    {
        var input = new Queue<Range>(ranges);
        var output = new List<Range>();

        while (input.Count != 0)
        {
            var range = input.Dequeue();
            var source = map.Keys.FirstOrDefault(x => x.Intersects(range));

            if (source is null)
            {
                output.Add(range);
            }
            else if (source.Start <= range.Start && range.End <= source.End)
            {
                var destination = map[source];
                var shift = destination.Start - source.Start;
                output.Add(new(range.Start + shift, range.End + shift));
            }
            else if (range.Start < source.Start)
            {
                input.Enqueue(range with { End = source.Start - 1 });
                input.Enqueue(new(source.Start, range.End));
            }
            else
            {
                input.Enqueue(new(range.Start, source.End));
                input.Enqueue(range with { Start = source.End + 1 });
            }
        }

        return output;
    }

    private static IEnumerable<Range> PuzzleOneRanges(IEnumerable<long> numbers) =>
        from n in numbers
        select new Range(n, n);

    private static IEnumerable<Range> PuzzleTwoRanges(IEnumerable<long> numbers) =>
        from n in numbers.Chunk(2)
        select new Range(n[0], n[0] + n[1] - 1);

    public override async Task RunAsync()
    {
        PuzzleOneResult(await SolveAsync(PuzzleOneRanges));
        PuzzleTwoResult(await SolveAsync(PuzzleTwoRanges));
    }

    private async Task<long> SolveAsync(Func<IEnumerable<long>, IEnumerable<Range>> parseSeeds)
    {
        var blocks = (await InternalReadAllTextAsync()).SplitByDoubleNewLine();
        var seedRanges = parseSeeds(ParseNumbers(blocks[0])).ToList();
        var maps = blocks.Skip(1).Select(ParseMap).ToArray();

        return maps.Aggregate(seedRanges, Project).Select(x => x.Start).Min();
    }

    #endregion
}

internal sealed record Range(long Start, long End)
{
    #region "Properties"

    public long Length => End - Start + 1;

    #endregion

    #region "Methods"

    public bool Intersects(Range other) =>
        Start <= other.End && other.Start <= End;

    #endregion
}
