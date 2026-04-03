namespace CardSharp;

/**
 *
 */
public class Card(Rank rank = Rank.Ace, Suit suit = Suit.Spades) : IComparable<Card>, IEquatable<Card>
{
    public Rank Rank = rank;
    public Suit Suit = suit;

    public int CompareTo(Card? other)
    {
        if (other is null)
            return 1;
        return Rank.CompareTo(other.Rank);
    }

    public bool Equals(Card? other) => other is not null && other.Rank == Rank && other.Suit == Suit;

    public bool IsOf(Rank rank, Suit suit) => Rank == rank && Suit == suit;
    
    public bool IsOf(Suit suit, Rank rank) => Rank == rank && Suit == suit;

    public bool IsOf(Rank rank) => Rank == rank;

    public bool IsOf(Suit suit) => Suit == suit;

    public override string ToString() => $"{Rank} of {Suit}";

    public char ToUnicodeChar()
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Card);
    }
}