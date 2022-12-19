using System.Collections;
using System.Diagnostics;

namespace BlackjackCsharpovy;

public class Deck
{
    public List<Card> Cards;

    internal Deck(int amountOfDecks)
    {
        Cards = new List<Card>();
        List<char> symbols = new List<char>()
        {
            '♣', '♦', '♥', '♠'
        };

        for (int amount = 0; amount < amountOfDecks; amount++)
        {
            for (int value = 1; value <= 13; value++)
            {
                for (int symbol = 0; symbol < 4; symbol++)
                {
                    string name;
                    switch (value)
                    {
                        case 1:
                            name = "A";
                            break;
                        case 11:
                            name = "J";
                            break;
                        case 12:
                            name = "Q";
                            break;
                        case 13:
                            name = "K";
                            break;
                        default:
                            name = value.ToString();
                            break;
                    }

                    Cards.Add(new Card(name, symbols[symbol]));
                }
            }
        }
        Shuffle();
    }
    
    private void Shuffle()
    {
        Random random = new Random();
        List<Card> shuffledCards = new List<Card>();
        int cardsCount = Cards.Count;
        
        for (int card = 0; card < cardsCount; card++)
        {
            int index = random.Next(0, Cards.Count);
            shuffledCards.Add(Cards[index]);
            Cards.RemoveAt(index);
        }
        Cards = shuffledCards;
    }

    public Card DealCard()
    {
        Random random = new Random();
        int randomIndex = random.Next(0, Cards.Count);
        
        Card card = Cards[randomIndex];
        Cards.RemoveAt(randomIndex);
        
        return card;
    }
}