namespace CardSharp.Poker;

public static class PokerUtils
{
    public static PokerHand DeterminePokerHand(IEnumerable<Card> hand)
    {
        var handEnumerable = hand as Card[] ?? hand.ToArray();
        if (handEnumerable.Length == 0)
            return PokerHand.HighCard;

        Span<int> rankCounts = stackalloc int[13];
        Span<int> suitCounts = stackalloc int[4];

        ulong rankMask = 0;

        foreach (Card card in handEnumerable)
        {
            int rank = (int)card.Rank;
            int suit = (int)card.Suit;

            rankCounts[rank]++;
            suitCounts[suit]++;
            rankMask |= 1UL << rank;
        }

        bool flush = false;
        Suit flushSuit = default;

        for (int suit = 0; suit < 4; suit++)
        {
            if (suitCounts[suit] >= 5)
            {
                flush = true;
                flushSuit = (Suit)suit;
                break;
            }
        }

        bool straight = HasStraight(rankMask);

        if (flush)
        {
            ulong flushMask = 0;

            foreach (Card card in handEnumerable)
            {
                if (card.Suit == flushSuit)
                    flushMask |= 1UL << (int)card.Rank;
            }

            bool straightFlush = HasStraight(flushMask);

            if (straightFlush)
            {
                ulong royalMask =
                    (1UL << (int)Rank.Ten) |
                    (1UL << (int)Rank.Jack) |
                    (1UL << (int)Rank.Queen) |
                    (1UL << (int)Rank.King) |
                    (1UL << (int)Rank.Ace);

                if ((flushMask & royalMask) == royalMask)
                    return PokerHand.RoyalFlush;

                return PokerHand.StraightFlush;
            }
        }

        int pairs = 0;
        bool trips = false;
        bool quads = false;

        foreach (int count in rankCounts)
        {
            switch (count)
            {
                case 4:
                    quads = true;
                    break;

                case 3:
                    trips = true;
                    break;

                case 2:
                    pairs++;
                    break;
            }
        }

        if (quads)
            return PokerHand.FourOfAKind;

        if (trips && pairs >= 1)
            return PokerHand.FullHouse;

        int tripCount = 0;

        foreach (int count in rankCounts)
        {
            if (count >= 3)
                tripCount++;
        }

        if (tripCount >= 2)
            return PokerHand.FullHouse;

        if (flush)
            return PokerHand.Flush;

        if (straight)
            return PokerHand.Straight;

        if (trips)
            return PokerHand.ThreeOfAKind;

        if (pairs >= 2)
            return PokerHand.TwoPair;

        if (pairs == 1)
            return PokerHand.Pair;

        return PokerHand.HighCard;
    }

    private static bool HasStraight(ulong rankMask)
    {
        // Wheel: A2345
        ulong wheel =
            (1UL << (int)Rank.Ace) |
            (1UL << (int)Rank.Two) |
            (1UL << (int)Rank.Three) |
            (1UL << (int)Rank.Four) |
            (1UL << (int)Rank.Five);

        if ((rankMask & wheel) == wheel)
            return true;

        for (int start = 0; start <= 8; start++)
        {
            ulong needed =
                (1UL << start) |
                (1UL << (start + 1)) |
                (1UL << (start + 2)) |
                (1UL << (start + 3)) |
                (1UL << (start + 4));

            if ((rankMask & needed) == needed)
                return true;
        }

        return false;
    }
}