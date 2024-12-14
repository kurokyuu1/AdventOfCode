namespace AdventOfCode.Riddles._2022.Models.DayFive;

internal sealed record OrderCommand(int Quantity, int From, int To);

internal sealed record StackData(IEnumerable<char>[] Stacks, OrderCommand[] Commands);
