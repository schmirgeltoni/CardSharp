using CardSharp;

namespace CardSharpTest;

public static class TestUtils
{
    public static readonly int StandardDeckSize =
        Enum.GetNames(typeof(Suit)).Length * Enum.GetNames(typeof(Rank)).Length;

    public static Deck DeckWithAllSpadeCards()
    {
        return new Deck(
            new Card(Rank.Two),
            new Card(Rank.Three),
            new Card(Rank.Four),
            new Card(Rank.Five),
            new Card(Rank.Six),
            new Card(Rank.Seven),
            new Card(Rank.Eight),
            new Card(Rank.Nine),
            new Card(Rank.Ten),
            new Card(Rank.Jack),
            new Card(Rank.Queen),
            new Card(Rank.King),
            new Card()
        );
    }

    public static void CheckAllValues<TEnum, TResult>(
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