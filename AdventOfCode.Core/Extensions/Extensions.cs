using System.Numerics;
using System.Text.RegularExpressions;
using System.Text;

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

    public static void LogMatches(this MatchCollection matches)
    {
        var sb = new StringBuilder();
        foreach (Match match in matches)
        {
            sb.AppendLine($"Match: '{match.Value}' at index {match.Index}");

            for (var i = 0; i < match.Groups.Count; i++)
            {
                sb.AppendLine($"\tGroup {i}: '{match.Groups[i].Value}'");
            }
        }

        File.WriteAllText("match_result_day3.txt", sb.ToString());
    }
}