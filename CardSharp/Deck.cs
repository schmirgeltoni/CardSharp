using System.Collections;

namespace CardSharp;

public class Deck : IEnumerable<Card>
{
    private readonly Card[] cards;
    private int deckPointer;
    public int Count => cards.Length - deckPointer;

    public Card[] DrawnCards => cards[..deckPointer];

    public Card[] Cards => cards[deckPointer..];

    private static readonly Card[] CardsInOrder =
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
        cards = CardsInOrder;
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

    public Deck(params Suit[] includedSuits)
    {
        cards = OrderedCards(includedSuits);
    }

    public Deck(IEnumerable<Suit> includedSuits)
    {
        cards = OrderedCards(includedSuits);
    }

    public Deck(params Rank[] includedRanks)
    {
        cards = OrderedCards(includedRanks: includedRanks);
    }

    public Deck(IEnumerable<Rank> includedRanks)
    {
        cards = OrderedCards(includedRanks: includedRanks);
    }

    public Deck(IEnumerable<Suit> includedSuits,
        IEnumerable<Rank> includedRanks)
    {
        cards = OrderedCards(includedSuits, includedRanks);
    }

    public Deck(IEnumerable<Rank> includedRanks, IEnumerable<Suit> includedSuits)
        : this(includedSuits, includedRanks) { }

    public static implicit operator Deck(List<Card> cards) => new(cards.ToArray());
    public static implicit operator Deck(Span<Card> cards) => new(cards.ToArray());
    public static implicit operator Deck(Card[] cards) => new(cards);

    /**
     * Draws the top most card of the deck.
     * Does not check whether drawing is even possible.
     */
    public Card Draw() => cards[deckPointer++];

    /*
     * Attempts to draw the top most card of the deck.
     * Returns true and the card as an out parameter if possible, false and null if not.
     */
    public bool TryDraw(out Card? card)
    {
        if (deckPointer >= cards.Length)
        {
            card = null;
            return false;
        }

        card = cards[deckPointer++];
        return true;
    }
    
    /**
     * Burns the top most card of the deck.
     * https://en.wikipedia.org/wiki/Burn_card
     */
    public void Burn()
    {
        deckPointer++;
    }

    public Card? Peek()
    {
        TryDraw(out var c);
        return c;
    }

    private static Card[] OrderedCards(
        IEnumerable<Suit>? includedSuits = null,
        IEnumerable<Rank>? includedRanks = null)
    {
        // no filters => full deck
        if (includedSuits is null && includedRanks is null)
            return CardsInOrder;

        return CardsInOrder
            .Where(card =>
                (includedSuits is null || includedSuits.Contains(card.Suit)) &&
                (includedRanks is null || includedRanks.Contains(card.Rank)))
            .ToArray();
    }

    /**
     * Shuffles the deck and resets the drawn pile.
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
        return new Deck(CardsInOrder.Except(excluded));
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

    public IEnumerator<Card> GetEnumerator() => ((IEnumerable<Card>)Cards).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}