using System.Collections;

namespace CardSharp;

public class Deck : IEnumerable<Card>
{
    private readonly Card[] cards;
    private int deckPointer;
    public int Count => cards.Length - deckPointer;

    public Card[] DrawnCards => cards[..deckPointer];

    public IReadOnlyList<Card> Cards => cards[deckPointer..];

    public Deck(bool shuffled = false)
    {
        cards = OrderedCards();
        if (shuffled)
            Shuffle();
    }

    public Deck(params Card[] cards)
    {
        this.cards = cards;
    }

    public Deck(List<Card> cards)
    {
        this.cards = cards.ToArray();
    }

    public Deck(params Suit[] excludedSuits)
    {
        throw new NotImplementedException();
    }

    public Deck(params Rank[] excludedRanks)
    {
        throw new NotImplementedException();
    }
    public static implicit operator Deck(List<Card> cards) => new(cards);
    public static implicit operator Deck(Card[] cards) => new(cards);

    /**
     * Draws the top most card of the deck
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

    private static Card[] OrderedCards()
    {
        var ret = new Card[52];
        var index = 0;
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                ret[index++] = (rank, suit);
            }
        }

        return ret;
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

    public static Deck StandardDeckWithoutFollowingCards(params Card[] excluded)
    {
        throw new NotImplementedException();
    }

    public static Deck ShuffledDeck()
    {
        var deck = new Deck();
        deck.Shuffle();
        return deck;
    }
    
    public Card this[int index] => Cards[index];

    public IEnumerator<Card> GetEnumerator() => ((IEnumerable<Card>)cards).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}