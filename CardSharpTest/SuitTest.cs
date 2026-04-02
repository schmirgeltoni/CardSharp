using CardSharp;

namespace CardSharpTest;

public class SuitTest
{
    [Fact]
    public void UnicodeCharacter_Works()
    {
        Assert.Equal('♠', Suit.Spades.ToBlackChar());
        Assert.Equal('♥', Suit.Hearts.ToBlackChar());
        Assert.Equal('♦', Suit.Diamonds.ToBlackChar());
        Assert.Equal('♣', Suit.Clubs.ToBlackChar());

        Assert.Equal('♤', Suit.Spades.ToWhiteChar());
        Assert.Equal('♡', Suit.Hearts.ToWhiteChar());
        Assert.Equal('♢', Suit.Diamonds.ToWhiteChar());
        Assert.Equal('♧', Suit.Clubs.ToWhiteChar());
    }
}