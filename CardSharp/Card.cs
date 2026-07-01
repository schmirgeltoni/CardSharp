namespace CardSharp;

/**
 *
 */
public class Card(Rank rank, Suit suit) : IComparable<Card>, IEquatable<Card>
{
    public Rank Rank = rank;
    public Suit Suit = suit;

    public int CompareTo(Card? other)
    {
        if (other is null)
            return 1;
        return Rank.CompareTo(other.Rank);
    }

    public Card(Suit suit, Rank rank) : this(rank, suit) { }

    public static implicit operator Card((Rank rank, Suit suit) values) => new(values.rank, values.suit);

    public static implicit operator Card((Suit suit, Rank rank) values) => new(values.rank, values.suit);

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

    public override int GetHashCode() => ToString().GetHashCode();
}
