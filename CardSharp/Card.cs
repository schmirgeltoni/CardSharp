namespace CardSharp;

/**
 *
 */
public readonly struct Card : IComparable<Card>, IEquatable<Rank>, IEquatable<Suit>, IEquatable<Card>
{
    private const int BitShiftOffset = 3;

    private readonly byte value = 0;

    public Rank Rank => (Rank)(value >> BitShiftOffset);

    public Suit Suit => (Suit)(value & 0b0111);

    public int CompareTo(Card other)
    {
        var rank = Rank.CompareTo(other.Rank);
        return rank != 0 ? rank : Suit.CompareTo(other.Suit);
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

    public string ToUnicode()
    {
        var suitBase = Suit switch
        {
            Suit.Spades => 0x1F0A0,
            Suit.Hearts => 0x1F0B0,
            Suit.Diamonds => 0x1F0C0,
            Suit.Clubs => 0x1F0D0,
            _ => throw new ArgumentOutOfRangeException()
        };

        var rankOffset = Rank switch
        {
            Rank.Ace => 0x1,
            Rank.Two => 0x2,
            Rank.Three => 0x3,
            Rank.Four => 0x4,
            Rank.Five => 0x5,
            Rank.Six => 0x6,
            Rank.Seven => 0x7,
            Rank.Eight => 0x8,
            Rank.Nine => 0x9,
            Rank.Ten => 0xA,
            Rank.Jack => 0xB,
            Rank.Queen => 0xD,
            Rank.King => 0xE,
            _ => throw new ArgumentOutOfRangeException()
        };

        return char.ConvertFromUtf32(suitBase + rankOffset);
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

    public static bool operator <(Card left, Card right) => left.CompareTo(right) < 0;
    public static bool operator >(Card left, Card right) => left.CompareTo(right) > 0;
    public static bool operator <=(Card left, Card right) => left.CompareTo(right) <= 0;
    public static bool operator >=(Card left, Card right) => left.CompareTo(right) >= 0;

    public bool Equals(Rank other) => Rank == other;

    public bool Equals(Suit other) => Suit == other;

    public override bool Equals(object? obj)
    {
        return obj is Card card && Equals(card);
    }
}