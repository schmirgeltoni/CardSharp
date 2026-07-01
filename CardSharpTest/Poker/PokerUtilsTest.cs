using CardSharp;
using CardSharp.Poker;

namespace CardSharpTest.Poker;

public class PokerUtilsTest
{
    #region Basic Algorithm Correctness Checks

    [Fact]
    public void RoyalFlushIsRecognized()
    {
        Card[] royalFlush =
        [
            (Rank.Ace, Suit.Spades),
            (Rank.King, Suit.Spades),
            (Rank.Queen, Suit.Spades),
            (Rank.Jack, Suit.Spades),
            (Rank.Ten, Suit.Spades),
        ];

        Assert.Equal(PokerHand.RoyalFlush, PokerUtils.DeterminePokerHand(royalFlush));
    }

    [Fact]
    public void StraightFlushIsRecognized()
    {
        Card[] straightFlush =
        [
            (Rank.King, Suit.Hearts),
            (Rank.Queen, Suit.Hearts),
            (Rank.Jack, Suit.Hearts),
            (Rank.Ten, Suit.Hearts),
            (Rank.Nine, Suit.Hearts),
        ];

        Assert.Equal(PokerHand.StraightFlush, PokerUtils.DeterminePokerHand(straightFlush));
    }

    [Fact]
    public void FourOfAKindIsRecognized()
    {
        Card[] quads =
        [
            (Rank.Ace, Suit.Spades),
            (Rank.Ace, Suit.Hearts),
            (Rank.Ace, Suit.Clubs),
            (Rank.Ace, Suit.Diamonds),
            (Rank.Nine, Suit.Spades),
        ];

        Assert.Equal(PokerHand.FourOfAKind, PokerUtils.DeterminePokerHand(quads));
    }

    [Fact]
    public void FullHouseIsRecognized()
    {
        Card[] fullHouse =
        [
            (Rank.Ace, Suit.Spades),
            (Rank.Ace, Suit.Hearts),
            (Rank.Ace, Suit.Clubs),
            (Rank.King, Suit.Diamonds),
            (Rank.King, Suit.Hearts),
        ];

        Assert.Equal(PokerHand.FullHouse, PokerUtils.DeterminePokerHand(fullHouse));
    }

    [Fact]
    public void FlushIsRecognized()
    {
        Card[] flush =
        [
            (Rank.Ace, Suit.Clubs),
            (Rank.Five, Suit.Clubs),
            (Rank.Ten, Suit.Clubs),
            (Rank.King, Suit.Clubs),
            (Rank.Seven, Suit.Clubs),
        ];

        Assert.Equal(PokerHand.Flush, PokerUtils.DeterminePokerHand(flush));
    }

    [Fact]
    public void StraightIsRecognized()
    {
        Card[] straight =
        [
            (Rank.Two, Suit.Hearts),
            (Rank.Three, Suit.Spades),
            (Rank.Four, Suit.Hearts),
            (Rank.Five, Suit.Diamonds),
            (Rank.Six, Suit.Clubs),
        ];

        Assert.Equal(PokerHand.Straight, PokerUtils.DeterminePokerHand(straight));
    }

    [Fact]
    public void ThreeOfAKindIsRecognized()
    {
        Card[] threeOfAKind =
        [
            (Rank.Ace, Suit.Spades),
            (Rank.Ace, Suit.Hearts),
            (Rank.Ace, Suit.Clubs),
            (Rank.King, Suit.Diamonds),
            (Rank.Queen, Suit.Hearts),
        ];

        Assert.Equal(PokerHand.ThreeOfAKind, PokerUtils.DeterminePokerHand(threeOfAKind));
    }

    [Fact]
    public void TwoPairIsRecognized()
    {
        Card[] twoPair =
        [
            (Rank.Ace, Suit.Spades),
            (Rank.Ace, Suit.Hearts),
            (Rank.King, Suit.Diamonds),
            (Rank.King, Suit.Hearts),
            (Rank.Queen, Suit.Clubs),
        ];

        Assert.Equal(PokerHand.TwoPair, PokerUtils.DeterminePokerHand(twoPair));
    }

    [Fact]
    public void PairIsRecognized()
    {
        Card[] pair =
        [
            (Rank.Ace, Suit.Spades),
            (Rank.Ace, Suit.Hearts),
            (Rank.Five, Suit.Diamonds),
            (Rank.Seven, Suit.Hearts),
            new(Rank.Queen, Suit.Clubs),
        ];

        Assert.Equal(PokerHand.Pair, PokerUtils.DeterminePokerHand(pair));
    }

    #endregion

    [Fact]
    public void LessThanFiveCardHandsReturnCorrectly()
    {
        Assert.Equal(PokerHand.HighCard, PokerUtils.DeterminePokerHand([(Rank.Ace, Suit.Spades)]));

        Card[] pair = [(Rank.Ace, Suit.Spades), (Rank.Ace, Suit.Hearts)];
        Assert.Equal(PokerHand.Pair, PokerUtils.DeterminePokerHand(pair));

        Card[] pairWithExtraCard = [(Rank.Ace, Suit.Spades), (Rank.Ace, Suit.Hearts), (Rank.Two, Suit.Diamonds)];
        Assert.Equal(PokerHand.Pair, PokerUtils.DeterminePokerHand(pairWithExtraCard));

        Card[] set = [(Rank.Ace, Suit.Spades), (Rank.Ace, Suit.Hearts), (Rank.Ace, Suit.Diamonds)];
        Assert.Equal(PokerHand.ThreeOfAKind, PokerUtils.DeterminePokerHand(set));
    }

    [Fact]
    public void HandContainingStraightAndFlushButNotStraightFlushReturnsFlush()
    {
        Card[] hand =
        [
            (Rank.King,Suit.Hearts),
            (Rank.Queen,Suit.Diamonds),
            (Rank.Jack,Suit.Spades),
            (Rank.Ten,Suit.Spades),
            (Rank.Nine,Suit.Spades),
            (Rank.Two,Suit.Spades),
            (Rank.Five,Suit.Spades),
        ];
        
        Assert.Equal(PokerHand.Flush ,PokerUtils.DeterminePokerHand(hand));
        Assert.NotEqual(PokerHand.Straight ,PokerUtils.DeterminePokerHand(hand));
        Assert.NotEqual(PokerHand.StraightFlush ,PokerUtils.DeterminePokerHand(hand));
    }
    
    [Fact]
    public void RoyalFlushAndStraightFlushIsDifferentiated()
    {
        Card[] veryLargeStraightFlush =
        [
            (Rank.Ace, Suit.Spades),
            (Rank.King, Suit.Spades),
            (Rank.Queen, Suit.Spades),
            (Rank.Jack, Suit.Spades),
            (Rank.Ten, Suit.Spades),
            (Rank.Nine, Suit.Spades),
            (Rank.Eight, Suit.Spades),
        ];

        Assert.Equal(PokerHand.RoyalFlush, PokerUtils.DeterminePokerHand(veryLargeStraightFlush));
        Assert.NotEqual(PokerHand.StraightFlush, PokerUtils.DeterminePokerHand(veryLargeStraightFlush));
    }
}