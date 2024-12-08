#region "Usings"

using MoreLinq;
using Newtonsoft.Json.Linq;

#endregion

namespace AdventOfCode._2022.Models.DayThirteen;

internal sealed class CompareToken : IComparable<CompareToken>
{
    #region "Properties"

    public int Index { get; set; }

    private JToken Token { get; }

    #endregion

    #region "Constructor"

    public CompareToken(string json) => Token = JToken.Parse(json);

    public CompareToken(JToken token) => Token = token;

    #endregion

    #region "Interface Methods"

    public int CompareTo(CompareToken? other) => Compare(Token, other!.Token);

    #endregion

    #region "Methods"

    private static int Compare(JToken left, JToken right)
    {
        if (left.Type == JTokenType.Integer && right.Type == JTokenType.Integer)
        {
            return left.Value<int>().CompareTo(right.Value<int>());
        }

        if (left.Type != right.Type)
        {
            if (left.Type == JTokenType.Integer)
            {
                left = new JArray(left);
            }
            else if (right.Type == JTokenType.Integer)
            {
                right = new JArray(right);
            }
        }


        foreach (var p in left.ZipLongest(right, (l, r) => (l, r)))
        {
            if (p.l is null)
            {
                return -1;
            }

            if (p.r is null)
            {
                return 1;
            }

            var compare = Compare(p.l, p.r);

            if (compare != 0)
            {
                return compare;
            }
        }

        return 0;
    }

    #endregion
}
