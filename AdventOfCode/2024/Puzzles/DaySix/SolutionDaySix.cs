using System.Collections.Specialized;
using AdventOfCode.Core.Enumeration;

namespace AdventOfCode._2024.Puzzles.DaySix;

[AdventModule("Day Six")]
public sealed class SolutionDaySix : SolutionBase
{
    public SolutionDaySix() : base("2024", "07", true)
    {
    }

    private const string SpaceSeparator = " ";

    public override async Task RunAsync()
    {
        var lines = await InternalReadAllLinesAsync(ReadingMode.TestInput);

        var sum = lines.Sum(SolveEquation);

        PuzzleOneResult(sum);
    }

    private int SolveEquation(string line)
    {
        var split = line.Split(":");
        var result = split[0];
        var numbers = split[1].Split(SpaceSeparator, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var counter = 0;
        return counter;
    }
}
