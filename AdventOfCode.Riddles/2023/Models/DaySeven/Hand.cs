namespace AdventOfCode.Riddles._2023.Models.DaySeven;

internal sealed record Hand(string Cards, int Bid, bool IsPartTwo = false) : IComparable<Hand>
{
    #region "Properties"

    public int CardValue => Bid * (int)Rank;

    public bool IsHighCard
    {
        get
        {
            var groups = Cards.GroupBy(c => c).Select(g => g.Count()).ToList();
            return groups.Count == 5;
        }
    }

    private char[] CardsList => [.. Cards];

    //public int Rank { get; init; }
    private bool IsFiveOfKind => Cards.Distinct().Count() == 1;
    private bool IsFourOfKind => Cards.GroupBy(c => c).Any(g => g.Count() == 4);

    private bool IsFullHouse
    {
        get
        {
            var groups = Cards.GroupBy(c => c).Select(g => g.Count()).ToList();

            return
                IsPartTwo ? groups.Contains(3) : groups.Contains(2) && groups.Contains(3);
        }
    }

    private bool IsOnePair
    {
        get
        {
            var groups = Cards.GroupBy(c => c).Select(g => g.Count()).ToList();
            return groups.Contains(2);
        }
    }

    private bool IsThreeOfKind => Cards.GroupBy(c => c).Any(g => g.Count() == 3);

    private bool IsTwoPairs
    {
        get
        {
            var groups = Cards.GroupBy(c => c).Select(g => g.Count()).ToList();
            return groups.Count(g => g == 2) == 2;
        }
    }

    private HandRank Rank
    {
        get
        {
            if (IsFiveOfKind)
            {
                return HandRank.FiveOfKind;
            }

            if (IsFourOfKind)
            {
                return HandRank.FourOfKind;
            }

            if (IsFullHouse)
            {
                return HandRank.FullHouse;
            }

            if (IsThreeOfKind)
            {
                return HandRank.ThreeOfKind;
            }

            if (IsTwoPairs)
            {
                return HandRank.TwoPairs;
            }

            return IsOnePair ? HandRank.OnePair : HandRank.HighCard;
        }
    }

    #endregion

    #region "Interface Methods"

    public int CompareTo(Hand? other)
    {
        if (other is null)
        {
            return 1;
        }

        if (Rank == other.Rank)
        {
            foreach (var (first, second) in CardsList.Zip(other.CardsList))
            {
                var v1 = CardStrength(first);
                var v2 = CardStrength(second);
                if (v1 != v2)
                {
                    return v1.CompareTo(v2);
                }
            }
        }

        return Rank.CompareTo(other.Rank);
    }

    #endregion

    #region "Methods"

    private int CardStrength(char c)
    {
        return c switch
        {
            'T' => 10,
            'J' => IsPartTwo ? 0 : 11,
            'Q' => 12,
            'K' => 13,
            'A' => 14,
            var _ => c - '0',
        };
    }

    #endregion
}
