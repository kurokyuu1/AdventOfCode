#region "Usings"

using System.Runtime.InteropServices;
using AdventOfCode.Core.RegularExpressions;
using AdventOfCode.Riddles._2022.Models.DayEleven;

#endregion

namespace AdventOfCode.Riddles._2022.Puzzles.DayEleven;

[AdventModule("Day Eleven")]
internal sealed class SolutionDayEleven : SolutionBase
{
    #region "Constants"

    private const int MaxRounds = 20;
    private const int PaddingWidth = 4;
    private const int SmallPaddingWidth = 2;

    private static readonly int OperationStartIndex = "  Operation: new = ".Length;

    #endregion

    #region "Constructor"

    public SolutionDayEleven() : base("2022", "11", false)
    {
    }

    #endregion

    #region "Methods"

    private static long ComputeMonkeyBusiness(IEnumerable<Monkey?> lst) =>
        lst
            .OrderByDescending(x => x!.Inspections)
            .Take(2)
            .Select(x => x!.Inspections)
            .Aggregate(1L, (x, y) => x * y);

    private void ComputeWorryLevel(ref long value, WorryOperation worryOperation, long divider,
        bool isModulo = false)
    {
        var hasOperationValue = worryOperation.Value.HasValue;
        var text = !hasOperationValue ? "itself" : worryOperation.Value.ToString();
        var translatedInstruction = worryOperation.Instruction switch
        {
            WorryOperationInstruction.Add => $"increased by {text}",
            WorryOperationInstruction.Multiply => $"multiplied by {text}",
            WorryOperationInstruction.Divide => $"divided by {text}",
            WorryOperationInstruction.Subtract => $"decreased by {text}",
            var _ => throw new ArgumentException("No valid instruction!", nameof(worryOperation.Instruction)),
        };

        var temp = worryOperation.Instruction switch
        {
            WorryOperationInstruction.Add => hasOperationValue ? worryOperation.Value!.Value + value : value + value,
            WorryOperationInstruction.Divide => hasOperationValue ? worryOperation.Value!.Value / value : 1,
            WorryOperationInstruction.Multiply => hasOperationValue
                ? worryOperation.Value!.Value * value
                : value * value,
            WorryOperationInstruction.Subtract => hasOperationValue ? worryOperation.Value!.Value - value : 0,
            var _ => throw new ArgumentException("No valid instruction!", nameof(worryOperation.Instruction)),
        };

        LogToConsole($"Worry level is {translatedInstruction} to {temp}".PadLeft(PaddingWidth));

        value = isModulo ? temp % divider : temp / divider;
    }

    private static WorryOperation ConvertToOperation(IReadOnlyList<string> input)
    {
        if (input.Count != 3)
        {
            throw new ArgumentOutOfRangeException(nameof(input),
                "Input string list should only contain three entries.");
        }

        var instruction = input[1] switch
        {
            "+" => WorryOperationInstruction.Add,
            "-" => WorryOperationInstruction.Subtract,
            "/" => WorryOperationInstruction.Divide,
            var _ => WorryOperationInstruction.Multiply,
        };

        if (input[2] == "old")
        {
            return new() { Instruction = instruction };
        }

        return new() { Value = input[2].ToLong(), Instruction = instruction };
    }

    private static Monkey? CreateMonkey(string input)
    {
        try
        {
            var lines = input.SplitByNewLine();
            var monkey = new Monkey
            {
                Name = lines[0][..^1],
                StartingItems = RegExCollection.ExtractNumbersRegex().Matches(lines[1]).Select(x => long.Parse(x.Value))
                    .ToList(),
                WorryOperation = ConvertToOperation(lines[2][OperationStartIndex..].Split(" ")),
                Test = ConvertToInt(lines[3]),
                TestIfTrue = ConvertToInt(lines[4]),
                TestIfFalse = ConvertToInt(lines[5]),
            };

            return monkey;
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);

            return default;
        }
    }

    private List<Monkey> GetInspectedMonkeys(List<Monkey> monkeys, int rounds, long divider, bool isMod = false)
    {
        for (var round = 0; round < rounds; round++)
        {
            LogToConsole($"I'm in Round {round + 1}!");

            foreach (var t in CollectionsMarshal.AsSpan(monkeys))
            {
                LogToConsole($"{t.Name}:");

                for (var j = 0; j < t.StartingItems.Count; j++)
                {
                    t.Inspections++;
                    var item = t.StartingItems[j];

                    LogToConsole($"Monkey inspects an item with a worry level of {item}".PadLeft(SmallPaddingWidth));

                    ComputeWorryLevel(ref item, t.WorryOperation, divider, isMod);

                    LogToConsole(
                        $"Monkey gets bored with item. Worry level is divided by {divider} to {item}".PadLeft(
                            PaddingWidth));

                    int whichMonkey;

                    if (item % t.Test != 0)
                    {
                        LogToConsole($"Current worry level is not divisible by {t.Test}".PadLeft(PaddingWidth));
                        monkeys[t.TestIfFalse].StartingItems.Add(item);
                        whichMonkey = t.TestIfFalse;
                    }
                    else
                    {
                        LogToConsole($"Current worry level is divisible by {t.Test}".PadLeft(PaddingWidth));
                        monkeys[t.TestIfTrue].StartingItems.Add(item);
                        whichMonkey = t.TestIfTrue;
                    }

                    LogToConsole(
                        $"Item with worry level {item} is thrown to monkey {whichMonkey}.".PadLeft(PaddingWidth));
                    t.StartingItems[j] = 0;
                }

                t.StartingItems = t.StartingItems.Where(x => x > 0).ToList();
            }
        }

        return monkeys;
    }

    private static List<Monkey?> ParseMonkeyList(string input) =>
        input
            .SplitByDoubleNewLine()
            .Select(CreateMonkey)
            .Where(x => x != null)
            .ToList();

    public override async Task RunAsync()
    {
        var input = await InternalReadAllTextAsync();
        var parsed = ParseMonkeyList(input);

        SolvePartOne(parsed);
        SolvePartTwo(parsed);
    }

    private void SolvePartOne(List<Monkey?> list)
    {
        var result = GetInspectedMonkeys(list!, MaxRounds, 3);

        foreach (var item in CollectionsMarshal.AsSpan(result))
        {
            WriteLine($"{item.Name} inspected items {item.Inspections} times.");
        }

        PuzzleOneResult(ComputeMonkeyBusiness(result));
    }

    private void SolvePartTwo(List<Monkey?> list)
    {
        var mod = list.Aggregate(1L, (m, t) => m * t!.Test);
        var result2 = GetInspectedMonkeys(list!, 10_000, mod, true);
        foreach (var item in CollectionsMarshal.AsSpan(result2))
        {
            WriteLine($"{item.Name} inspected items {item.Inspections} times.");
        }

        PuzzleTwoResult(ComputeMonkeyBusiness(result2));
    }

    #endregion
}
