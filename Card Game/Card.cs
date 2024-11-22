public class Game
{
    private readonly Deck _deck;
    private Card _currentCard;
    private Card _nextCard;
    public int Score { get; private set; }
    public int CurrentRound { get; private set; }

    public Card CurrentCard => _currentCard;
    public Card NextCard => _nextCard;

    public Game(bool includeJokers = false)
    {
        _deck = new Deck(includeJokers);
        Score = 0;
        CurrentRound = 0;
        _currentCard = _deck.DrawCard();
        _nextCard = _deck.DrawCard();
    }

    public bool CheckGuess(bool playerGuessedHigher)
    {
        int currentCardValue = GetCardValue(_currentCard);
        int nextCardValue = GetCardValue(_nextCard);

        bool isHigher = nextCardValue > currentCardValue;

        return (playerGuessedHigher && isHigher) || (!playerGuessedHigher && !isHigher);
    }

    private int GetCardValue(Card card)
    {
        return card.Rank switch
        {
            "Ace" => 14,
            "King" => 13,
            "Queen" => 12,
            "Jack" => 11,
            "10" => 10,
            "9" => 9,
            "8" => 8,
            "7" => 7,
            "6" => 6,
            "5" => 5,
            "4" => 4,
            "3" => 3,
            "2" => 2,
            _ => 0
        };
    }

    public void AdvanceToNextRound()
    {
        _currentCard = _nextCard;
        _nextCard = _deck.DrawCard();
        CurrentRound++;
        if (CurrentRound > 0) Score++;
    }
}

public class Card
{
    public string Rank { get; set; }
    public string Suit { get; set; }

    public Card(string rank, string suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

public class Deck
{
    private static readonly string[] Ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
    private static readonly string[] Suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
    public List<Card> Cards { get; private set; }

    public Deck(bool includeJokers = false)
    {
        Cards = new List<Card>();

        foreach (var suit in Suits)
        {
            foreach (var rank in Ranks)
            {
                Cards.Add(new Card(rank, suit));
            }
        }

        // Optionally add jokers
        if (includeJokers)
        {
            Cards.Add(new Card("Joker", "Red"));
            Cards.Add(new Card("Joker", "Black"));
        }

        Shuffle();
    }

    public void Shuffle()
    {
        Random rng = new Random();
        int n = Cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var temp = Cards[k];
            Cards[k] = Cards[n];
            Cards[n] = temp;
        }
    }

    public Card DrawCard()
    {
        if (Cards.Any())
        {
            var card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
        return null;
    }
}