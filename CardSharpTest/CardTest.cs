using CardSharp;

namespace CardSharpTest;

public class CardTest
{
    [Fact]
    public void CardComparisons_Works()
    {
        var aceOfSpades = new Card();
        var deuceOfHearts = new Card(Rank.Two, Suit.Hearts);

        Assert.True(aceOfSpades.CompareTo(deuceOfHearts) > 0);
    }

    [Fact]
    public void CardEquality_Works()
    {
        var aceOfSpades1 = new Card();
        var aceOfSpades2 = new Card();

        Assert.False(aceOfSpades1 == aceOfSpades2);

        Assert.True(aceOfSpades1.Equals(aceOfSpades2));
    }

    [Fact]
    public void ToStringOverrideWorks()
    {
        Assert.Equal("Ace of Spades", new Card().ToString());
    }
}