using CardSharp;

namespace CardSharpTest;

public class DeckTest
{
    private static readonly List<Card> ShuffledDeckWithSpecifiedSeed =
    [
        new(Rank.Ace, Suit.Diamonds),
        new(Rank.Queen, Suit.Diamonds),
        new(Rank.Ten, Suit.Spades),
        new(Rank.Two, Suit.Clubs),
        new(Rank.Seven, Suit.Clubs),
        new(Rank.Nine, Suit.Hearts),
        new(Rank.Four, Suit.Diamonds),
        new(Rank.Two, Suit.Hearts),
        new(Rank.Jack, Suit.Clubs),
        new(Rank.Five, Suit.Clubs),
        new(Rank.Eight, Suit.Diamonds),
        new(Rank.Ten, Suit.Diamonds),
        new(Rank.Six, Suit.Clubs),
        new(Rank.Five, Suit.Diamonds),
        new(Rank.Four, Suit.Hearts),
        new(Rank.Eight, Suit.Clubs),
        new(Rank.Six, Suit.Spades),
        new(Rank.King, Suit.Hearts),
        new(Rank.Two, Suit.Diamonds),
        new(Rank.Three, Suit.Hearts),
        new(Rank.Seven, Suit.Diamonds),
        new(Rank.King, Suit.Spades),
        new(Rank.Nine, Suit.Diamonds),
        new(Rank.Jack, Suit.Diamonds),
        new(Rank.Nine, Suit.Clubs),
        new(Rank.Ten, Suit.Hearts),
        new(Rank.Three, Suit.Clubs),
        new(Rank.Nine, Suit.Spades),
        new(Rank.Seven, Suit.Hearts),
        new(Rank.Jack, Suit.Spades),
        new(Rank.Five, Suit.Spades),
        new(Rank.Jack, Suit.Hearts),
        new(Rank.Four, Suit.Spades),
        new(Rank.Six, Suit.Diamonds),
        new(Rank.Ace, Suit.Spades),
        new(Rank.Eight, Suit.Spades),
        new(Rank.Queen, Suit.Spades),
        new(Rank.Three, Suit.Spades),
        new(Rank.Queen, Suit.Hearts),
        new(Rank.Eight, Suit.Hearts),
        new(Rank.Three, Suit.Diamonds),
        new(Rank.Seven, Suit.Spades),
        new(Rank.Four, Suit.Clubs),
        new(Rank.Six, Suit.Hearts),
        new(Rank.King, Suit.Clubs),
        new(Rank.Two, Suit.Spades),
        new(Rank.Queen, Suit.Clubs),
        new(Rank.Five, Suit.Hearts),
        new(Rank.King, Suit.Diamonds),
        new(Rank.Ace, Suit.Clubs),
        new(Rank.Ten, Suit.Clubs),
        new(Rank.Ace, Suit.Hearts),
    ];

    [Fact]
    public void NormalNewDeck_ShouldHave52Cards()
    {
        var deck = new Deck();

        Assert.Equal(TestUtils.StandardDeckSize, deck.Count);
    }

    [Fact]
    public void StandardDeck_IsInOrder()
    {
        var deck = new Deck();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                Assert.True(deck.ElementAt(i * 13 + j).Equals(new Card((Rank)j, (Suit)i)));
            }
        }
    }

    [Fact]
    public void DeckShouldGetSmaller_AfterDraw()
    {
        var deck = new Deck();
        var oldSize = deck.Count;
        var drawnCard = deck.Draw();

        Assert.True(drawnCard.Equals(new Card(Rank.Two, Suit.Spades)));

        Assert.Equal(oldSize - 1, deck.Count);
    }

    [Fact]
    public void DeckShouldGetSmaller_AfterBurn()
    {
        var deck = new Deck();
        var oldSize = deck.Count;
        deck.Burn();

        Assert.Equal(oldSize - 1, deck.Count);
    }

    [Fact]
    public void DeckShuffleRandomizesCards()
    {
        var predeterminedRandom = new Random(69420);
        var deck = new Deck();
        deck.Shuffle(predeterminedRandom);
        var index = 0;
        foreach (var card in deck)
        {
            Assert.True(card.Equals(ShuffledDeckWithSpecifiedSeed.ElementAt(index++)));
        }
    }

    [Fact]
    public void ShuffleResetsDrawnPileAndPutsItBack()
    {
        var deck = new Deck();
        var drawnCard = deck.Draw();

        Assert.Single(deck.DrawnCards);
        Assert.True(drawnCard.IsOf(Rank.Two, Suit.Spades));
        Assert.True(deck.DrawnCards[0].Equals(drawnCard));

        Assert.DoesNotContain(drawnCard, deck);

        deck.Shuffle();

        Assert.Empty(deck.DrawnCards);
        Assert.Contains(drawnCard, deck.Cards);
    }

    [Fact]
    public void NoDuplicateCards_AfterShuffle()
    {
        var deck = new Deck();
        var set = new HashSet<Card>();
        foreach (var card in deck)
        {
            set.Add(card);
        }

        Assert.Equal(TestUtils.StandardDeckSize, set.Count);
        deck.Shuffle();
        foreach (var card in deck)
        {
            Assert.Contains(card, set);
        }
    }
}
