using CardSharp;

namespace CardSharpTest;

public static class TestUtils
{
    public static readonly int StandardDeckSize =
        Enum.GetNames(typeof(Suit)).Length * Enum.GetNames(typeof(Rank)).Length;

    public static Deck DeckWithOnlySpadeCards()
    {
        return new Deck(
            new Card(Rank.Two, Suit.Spades),
            new Card(Rank.Three, Suit.Spades),
            new Card(Rank.Four, Suit.Spades),
            new Card(Rank.Five, Suit.Spades),
            new Card(Rank.Six, Suit.Spades),
            new Card(Rank.Seven, Suit.Spades),
            new Card(Rank.Eight, Suit.Spades),
            new Card(Rank.Nine, Suit.Spades),
            new Card(Rank.Ten, Suit.Spades),
            new Card(Rank.Jack, Suit.Spades),
            new Card(Rank.Queen, Suit.Spades),
            new Card(Rank.King, Suit.Spades),
            new Card(Rank.Ace, Suit.Spades)
        );
    }

    public static void CheckAllValuesOfEnum<TEnum, TResult>(
        Func<TEnum, TResult> function,
        params TResult[] expectedValues)
        where TEnum : struct, Enum
    {
        var values = Enum.GetValues<TEnum>();

        if (expectedValues.Length != values.Length)
            throw new ArgumentException(
                "Provided values do not match number of enum values");

        for (int i = 0; i < values.Length; i++)
        {
            Assert.Equal(expectedValues[i], function(values[i]));
        }
    }
}
