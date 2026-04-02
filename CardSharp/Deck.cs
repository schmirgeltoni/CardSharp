using System.Collections;

namespace CardSharp;

public class Deck : IEnumerable<Card>
{
    private readonly List<Card> cards;
    private readonly List<Card> drawnCards = [];
    public int Count => cards.Count;

    public IReadOnlyList<Card> DrawnCards => drawnCards;
    public IReadOnlyList<Card> Cards => cards;

    public Deck(int cardCount = 52)
    {
        cards = [];
        switch (cardCount)
        {
            case 52:
            {
                cards = OrderedCards();
                break;
            }
            case > 52:
            {
                Random rng = new Random();
                cards = OrderedCards();
                for (int i = cardCount; i > 52; i--)
                {
                    cards.Add(new Card((Rank)rng.Next(0, 13), (Suit)rng.Next(0, 4)));
                }

                break;
            }
            default:
                throw new NotImplementedException();
        }
    }

    public Deck(params Card[] cards)
    {
        this.cards = cards.ToList();
    }

    public Deck(List<Card> cards)
    {
        this.cards = cards;
    }

    public Deck(params Suit[] excludedSuits)
    {
        cards = OrderedCards();
        cards.RemoveAll(card => excludedSuits.Contains(card.Suit));
    }

    public Deck(params Rank[] excludedRanks)
    {
        cards = OrderedCards();
        cards.RemoveAll(card => excludedRanks.Contains(card.Rank));
    }

    public Card Draw()
    {
        if (Count == 0)
            throw new InvalidOperationException("Deck is empty.");
        var drawnCard = cards[0];
        drawnCards.Add(drawnCard);
        cards.RemoveAt(0);
        return drawnCard;
    }

    public bool TryDraw(out Card card)
    {
        if (Count > 0)
        {
            card = Draw();
            return true;
        }
        else
        {
            card = default!;
            return false;
        }
    }

    public void Burn()
    {
        if (Count == 0)
            return;
        drawnCards.Add(cards[0]);
        cards.RemoveAt(0);
    }

    private static List<Card> OrderedCards()
    {
        List<Card> ret = [];
        foreach (var suit in (Suit[])Enum.GetValues(typeof(Suit)))
        {
            foreach (var rank in (Rank[])Enum.GetValues(typeof(Rank)))
            {
                ret.Add(new Card(rank, suit));
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
        cards.AddRange(drawnCards);
        drawnCards.Clear();
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }
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

    public IEnumerator<Card> GetEnumerator()
    {
        return cards.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}