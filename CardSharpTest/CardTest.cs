using CardSharp;

namespace CardSharpTest;

public class CardTest
{
    [Fact]
    public void CardComparisons_Works()
    {
        var aceOfSpades = new Card(Rank.Ace, Suit.Spades);
        var deuceOfHearts = new Card(Rank.Two, Suit.Hearts);

        Assert.True(aceOfSpades.CompareTo(deuceOfHearts) > 0);
    }

    [Fact]
    public void CardEquality_Works()
    {
        var aceOfSpades1 = new Card(Rank.Ace, Suit.Spades);
        var aceOfSpades2 = new Card(Rank.Ace, Suit.Spades);
        var deuceOfHearts = new Card(Rank.Two, Suit.Hearts);
        Card? nullCard = null;

        Assert.True(aceOfSpades1.Equals(aceOfSpades2));
        Assert.False(aceOfSpades1.Equals(deuceOfHearts));
        Assert.False(aceOfSpades1.Equals(nullCard));
    }

    [Fact]
    public void ToStringOverrideWorks()
    {
        Assert.Equal("Ace of Spades", new Card(Rank.Ace, Suit.Spades).ToString());
    }
}