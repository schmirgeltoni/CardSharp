using System.Collections;

namespace CardSharp;

public class Deck : IEnumerable<Card>
{
    private readonly Card[] cards;
    private int deckPointer;
    public int Count => cards.Length - deckPointer;

    public Card[] DrawnCards => cards[..deckPointer];

    public IReadOnlyList<Card> Cards => cards[deckPointer..];

    private static readonly Card[] StandardDeckCards =
    [
        (Rank.Two, Suit.Spades),
        (Rank.Three, Suit.Spades),
        (Rank.Four, Suit.Spades),
        (Rank.Five, Suit.Spades),
        (Rank.Six, Suit.Spades),
        (Rank.Seven, Suit.Spades),
        (Rank.Eight, Suit.Spades),
        (Rank.Nine, Suit.Spades),
        (Rank.Ten, Suit.Spades),
        (Rank.Jack, Suit.Spades),
        (Rank.Queen, Suit.Spades),
        (Rank.King, Suit.Spades),
        (Rank.Ace, Suit.Spades),

        (Rank.Two, Suit.Hearts),
        (Rank.Three, Suit.Hearts),
        (Rank.Four, Suit.Hearts),
        (Rank.Five, Suit.Hearts),
        (Rank.Six, Suit.Hearts),
        (Rank.Seven, Suit.Hearts),
        (Rank.Eight, Suit.Hearts),
        (Rank.Nine, Suit.Hearts),
        (Rank.Ten, Suit.Hearts),
        (Rank.Jack, Suit.Hearts),
        (Rank.Queen, Suit.Hearts),
        (Rank.King, Suit.Hearts),
        (Rank.Ace, Suit.Hearts),

        (Rank.Two, Suit.Diamonds),
        (Rank.Three, Suit.Diamonds),
        (Rank.Four, Suit.Diamonds),
        (Rank.Five, Suit.Diamonds),
        (Rank.Six, Suit.Diamonds),
        (Rank.Seven, Suit.Diamonds),
        (Rank.Eight, Suit.Diamonds),
        (Rank.Nine, Suit.Diamonds),
        (Rank.Ten, Suit.Diamonds),
        (Rank.Jack, Suit.Diamonds),
        (Rank.Queen, Suit.Diamonds),
        (Rank.King, Suit.Diamonds),
        (Rank.Ace, Suit.Diamonds),

        (Rank.Two, Suit.Clubs),
        (Rank.Three, Suit.Clubs),
        (Rank.Four, Suit.Clubs),
        (Rank.Five, Suit.Clubs),
        (Rank.Six, Suit.Clubs),
        (Rank.Seven, Suit.Clubs),
        (Rank.Eight, Suit.Clubs),
        (Rank.Nine, Suit.Clubs),
        (Rank.Ten, Suit.Clubs),
        (Rank.Jack, Suit.Clubs),
        (Rank.Queen, Suit.Clubs),
        (Rank.King, Suit.Clubs),
        (Rank.Ace, Suit.Clubs),
    ];

    public Deck(bool shuffled = false)
    {
        cards = StandardDeckCards;
        if (shuffled)
            Shuffle();
    }

    public Deck(params Card[] cards)
    {
        this.cards = cards;
    }

    public Deck(IEnumerable<Card> cards)
    {
        this.cards = cards.ToArray();
    }

    public Deck(params Suit[] excludedSuits)
    {
        cards = OrderedCards(excludedSuits);
    }

    public Deck(params Rank[] excludedRanks)
    {
        cards = OrderedCards(null, excludedRanks);
    }

    public Deck(IEnumerable<Suit> excludedSuits, IEnumerable<Rank> excludedRanks)
    {
        cards = OrderedCards(excludedSuits, excludedRanks);
    }

    public Deck(IEnumerable<Rank> excludedRanks, IEnumerable<Suit> excludedSuits)
        : this(excludedSuits, excludedRanks) { }

    public static implicit operator Deck(List<Card> cards) => new(cards.ToArray());
    public static implicit operator Deck(Span<Card> cards) => new(cards.ToArray());
    public static implicit operator Deck(Card[] cards) => new(cards);

    /**
     * Draws the top most card of the deck.
     */
    public Card Draw() => cards[deckPointer++];

    public bool TryDraw(out Card card)
    {
        deckPointer++;
        if (deckPointer > cards.Length)
        {
            card = new Card();
            return false;
        }

        card = cards[deckPointer];
        return true;
    }

    public void Burn()
    {
        deckPointer++;
    }

    private static Card[] OrderedCards(IEnumerable<Suit>? excludedSuits = null, IEnumerable<Rank>? excludedRanks = null)
    {
        // for efficiency
        if (excludedSuits is not null && excludedRanks is not null)
            return StandardDeckCards;

        return StandardDeckCards.Where(card =>
            excludedSuits is null || !excludedSuits.Contains(card.Suit) || excludedRanks is null ||
            !excludedRanks.Contains(card.Rank)).ToArray();
    }

    /**
     * Puts every card that was already drawn
     * and every remaining card back into the deck and shuffles them.
     */
    public void Shuffle(Random? rng = null)
    {
        rng ??= new Random();
        int n = cards.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }

        deckPointer = 0;
    }

    public static Deck DeckWithoutFollowingCards(params Card[] excluded)
    {
        return new Deck(StandardDeckCards.Except(excluded));
    }

    public static Deck ShuffledDeck()
    {
        var deck = new Deck();
        deck.Shuffle();
        return deck;
    }

    /**
     *  Indexes the remaining, not drawn deck.
     */
    public Card this[int index] => Cards[index];

    public IEnumerator<Card> GetEnumerator() => ((IEnumerable<Card>)cards).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}