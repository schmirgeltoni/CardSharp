namespace CardSharp;

/**
 *
 */
public readonly struct Card : IComparable<Card>, IEquatable<Card>
{
    private const int BitShiftOffset = 3;
    
    private readonly byte value = 0;

    public Rank Rank => (Rank)(value >> BitShiftOffset);

    public Suit Suit => (Suit)(value & 0b0111);

    public int CompareTo(Card other)
    {
        return Rank.CompareTo(other.Rank);
    }

    public Card(Rank rank, Suit suit)
    {
        value = (byte)(((int)rank << BitShiftOffset) | (int)suit);
    }

    public Card(Suit suit, Rank rank) : this(rank, suit)
    {
    }

    public static implicit operator Card((Rank rank, Suit suit) values) => new(values.rank, values.suit);

    public static implicit operator Card((Suit suit, Rank rank) values) => new(values.rank, values.suit);

    public bool Equals(Card other) => other.value == value;

    public bool IsOf(Rank rank, Suit suit) => Rank == rank && Suit == suit;

    public bool IsOf(Suit suit, Rank rank) => Rank == rank && Suit == suit;

    public bool IsOf(Rank rank) => Rank == rank;

    public bool IsOf(Suit suit) => Suit == suit;

    public override string ToString() => $"{Rank} of {Suit}";

    public char ToUnicodeChar()
    {
        throw new NotImplementedException();
    }

    public override int GetHashCode() => value;

    public static bool operator ==(Card left, Card right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Card left, Card right)
    {
        return !(left == right);
    }

    public static bool operator <(Card left, Card right)
    {
        throw new NotImplementedException();
    }

    public static bool operator >(Card left, Card right)
    {
        throw new NotImplementedException();
    }
    
    public static bool operator <=(Card left, Card right)
    {
        throw new NotImplementedException();
    }
    public static bool operator >=(Card left, Card right)
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        return obj is Card card && Equals(card);
    }
}