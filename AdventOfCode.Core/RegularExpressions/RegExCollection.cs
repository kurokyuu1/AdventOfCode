using System.Text.RegularExpressions;

namespace AdventOfCode.Core.RegularExpressions;

public static partial class RegExCollection
{
    #region "Methods"

    [GeneratedRegex(@"\*", RegexOptions.Compiled)]
    public static partial Regex ExtractGears();

    [GeneratedRegex("\\d", RegexOptions.Compiled)]
    public static partial Regex ExtractNumberRegex();

    [GeneratedRegex(@"\d|one|two|three|four|five|six|seven|eight|nine", RegexOptions.Compiled)]
    public static partial Regex ExtractNumbersFromWordsRegex();

    [GeneratedRegex(@"\d|one|two|three|four|five|six|seven|eight|nine",
        RegexOptions.RightToLeft | RegexOptions.Compiled)]
    public static partial Regex ExtractNumbersFromWordsReversed();

    [GeneratedRegex("\\d+", RegexOptions.Compiled)]
    public static partial Regex ExtractNumbersRegex();

    [GeneratedRegex("[^.0-9]", RegexOptions.Compiled)]
    public static partial Regex ExtractSymbols();

    [GeneratedRegex("[A-Z]+", RegexOptions.Compiled)]
    public static partial Regex ExtractAZNodes();

    [GeneratedRegex(@"@(\w+)@([\w.-]+[.][a-z]{2,})", RegexOptions.Compiled)]
    public static partial Regex ReplaceSkyUserName();

    [GeneratedRegex(@"(http[s]?:\/\/[^\s]+)", RegexOptions.Compiled)]
    public static partial Regex ReplaceUrl();

    #endregion
}
