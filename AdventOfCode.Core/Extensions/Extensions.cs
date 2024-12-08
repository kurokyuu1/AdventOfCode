using System.Numerics;

namespace AdventOfCode.Core.Extensions;

public static class Extensions
{
    public static string[] SplitByNewLine(this string input) =>
        input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

    public static string[] SplitByDoubleNewLine(this string input) =>
        input.Split($"{Environment.NewLine}{Environment.NewLine}");

    public static string[] SplitBySpace(this string input) =>
        input.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

    public static int ToInt(this string input) => int.Parse(input);
    public static long ToLong(this string input) => long.Parse(input);

    public static void SortDescending<TNumber>(this List<TNumber> list) where TNumber : INumber<TNumber>
        => list.Sort((a, b) => b.CompareTo(a));

    public static void SortAscending<TNumber>(this List<TNumber> list) where TNumber : INumber<TNumber>
        => list.Sort((a, b) => a.CompareTo(b));
}