using System.Text.RegularExpressions;

namespace AdventOfCode._2024.RegularExpressions
{
    public static partial class RegexCollection
    {
        [GeneratedRegex(@"mul\([0-9]{1,3}\,[0-9]{1,3}\)", RegexOptions.Compiled)]
        public static partial Regex ExtractMul();


        [GeneratedRegex(@"(?<dont>don't\(\))|(?<do>do\(\))|(?<mul>mul\([0-9]{1,3},[0-9]{1,3}\))", RegexOptions.Compiled)]
        public static partial Regex ExtractDontDo();
    }
}
