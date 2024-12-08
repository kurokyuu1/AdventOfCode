using System.Text;

namespace AdventOfCode.Core.Extensions;

public static class StringExtensions
{
    private static readonly char[] Separator = ['.', '!', '?'];

    public static string[] SplitText(this string text, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return [];
        }

        var sentences = text.Split(Separator, StringSplitOptions.RemoveEmptyEntries);

        if (sentences.Length == 0)
        {
            return SplitByLength(text, maxLength);
        }

        var sb = new StringBuilder();
        var result = new List<string>();

        foreach (var sentence in sentences)
        {
            if (sb.Length + sentence.Length + 1 <= maxLength)
            {
                if (sb.Length > 0)
                {
                    sb.Append(' ');
                }
            }
            else
            {
                result.Add(sb.ToString());
                sb.Clear();
            }

            sb.Append(sentence);
        }

        if (sb.Length > 0)
        {
            result.Add(sb.ToString());
        }

        return [.. result];
    }

    public static string[] SplitByLength(this string text, int maxLength)
    {
        var result = new List<string>();
        var idx = 0;

        while (idx < text.Length)
        {
            var length = Math.Min(maxLength, text.Length - idx);
            result.Add(text[idx..length]);
            idx += length;
        }

        return [.. result];
    }
}
