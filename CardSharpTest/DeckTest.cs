using CardSharp;

namespace CardSharpTest;

public class DeckTest
{
    private static readonly Card[] ShuffledDeckWithSpecifiedSeed =
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
                Assert.Equal(((Rank)j, (Suit)i), deck.ElementAt(i * 13 + j));
            }
        }
    }

    [Fact]
    public void DeckShouldGetSmaller_AfterDraw()
    {
        var deck = new Deck();
        var oldSize = deck.Count;
        deck.Burn();

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
    public void TryDraw_OnFullDeck_ReturnsTrueAndACard()
    {
        var deck = new Deck();
        if (deck.TryDraw(out var c))
        {
            Assert.NotNull(c);
            Assert.Equal((Suit.Spades, Rank.Two), c);
        }
        else
        {
            Assert.Fail("This branch should never be reached");
        }

        Assert.Equal(TestUtils.StandardDeckSize - 1, deck.Count);
    }

    [Fact]
    public void TryDraw_OnEmptyDeck_ReturnsFalseAndNull()
    {
        var oneCardDeck = new Deck((Rank.Ace, Suit.Diamonds));
        oneCardDeck.Burn();

        Assert.Equal(0, oneCardDeck.Count);
        Assert.False(oneCardDeck.TryDraw(out var c1));
        Assert.Null(c1);
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
            Assert.Equal(ShuffledDeckWithSpecifiedSeed.ElementAt(index++), card);
        }
    }

    [Fact]
    public void ShuffleResetsDrawnPileAndPutsItBack()
    {
        var deck = new Deck();
        var drawnCard = deck.Draw();

        Assert.Single(deck.DrawnCards);
        Assert.Equal((Rank.Two, Suit.Spades), drawnCard);
        Assert.Equal(drawnCard, deck.DrawnCards[0]);

        Assert.DoesNotContain(drawnCard, deck.Cards);

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

    [Fact]
    public void IndexerWorks()
    {
        var deck = new Deck();
        var card = deck[0];
        Assert.Equal((Rank.Two, Suit.Spades), card);
        deck.Burn();
        card = deck[0];

        Assert.Equal((Rank.Three, Suit.Spades), card);
        Assert.NotEqual((Rank.Two, Suit.Spades), card);
    }

    [Fact]
    public void InclusionConstructorsWork()
    {
        var deckWithOnlySpades = new Deck(Suit.Spades);
        Assert.Equal(TestUtils.NumberOfRanks, deckWithOnlySpades.Count);
        Assert.All(deckWithOnlySpades, card => Assert.True(card.Suit == Suit.Spades));

        var deckWithOnlyFaceCards = new Deck(Rank.King, Rank.Queen, Rank.Jack);
        Assert.Equal(3 * TestUtils.NumberOfSuits, deckWithOnlyFaceCards.Count);
        Assert.All(deckWithOnlyFaceCards, card => Assert.True(card.Rank is > Rank.Ten and < Rank.Ace));

        List<Rank> evenRanks = [Rank.Two, Rank.Four, Rank.Six, Rank.Eight, Rank.Ten];
        List<Suit> redSuits = [Suit.Hearts, Suit.Diamonds];

        var deckWithOnlyEvenRedCards = new Deck(redSuits, evenRanks);
        Assert.Equal(evenRanks.Count * redSuits.Count, deckWithOnlyEvenRedCards.Count);
        Assert.All(deckWithOnlyEvenRedCards,
            card => Assert.True((int)card.Rank % 2 == 0 &&
                                card is { Rank: < Rank.Jack, Suit: Suit.Hearts or Suit.Diamonds }));
    }
}