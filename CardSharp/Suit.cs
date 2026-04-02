namespace CardSharp;

public enum Suit
{
    Spades,
    Hearts,
    Diamonds,
    Clubs
}

public static class SuitMethods
{
    /**
     * Returns the black Unicode character for the suit.
     * https://en.wikipedia.org/wiki/Playing_cards_in_Unicode
     */
    public static char ToBlackChar(this Suit suit) => suit switch
    {
        Suit.Spades => '\u2660',
        Suit.Hearts => '\u2665',
        Suit.Diamonds => '\u2666',
        Suit.Clubs => '\u2663',
        _ => throw new ArgumentOutOfRangeException(nameof(suit), suit, null)
    };

    /**
     * Returns the white Unicode character for the suit.
     * https://en.wikipedia.org/wiki/Playing_cards_in_Unicode
     */
    public static char ToWhiteChar(this Suit suit) => suit switch
    {
        Suit.Spades => '\u2664',
        Suit.Hearts => '\u2661',
        Suit.Diamonds => '\u2662',
        Suit.Clubs => '\u2667',
        _ => throw new ArgumentOutOfRangeException(nameof(suit), suit, null)
    };
}