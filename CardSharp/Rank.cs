namespace CardSharp;

public enum Rank
{
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

public static class RankMethods
{
    public static char ToChar(this Rank rank)
    {
        if (rank is >= Rank.Two and < Rank.Ten)
            return ((int)rank + 2).ToString()[0];

        return rank.ToString()[0];
    }

    public static int BlackJackValue(this Rank rank)
    {
        return rank switch
        {
            >= Rank.Two and < Rank.Ten => (int)rank + 2,
            >= Rank.Ten and < Rank.Ace => 10,
            Rank.Ace => 11,
            _ => throw new ArgumentOutOfRangeException($"Unknown rank {rank}")
        };
    }
}