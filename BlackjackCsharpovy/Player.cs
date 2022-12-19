using System.Diagnostics;

namespace BlackjackCsharpovy;

public class Player
{
    public string Name;
    public int Money;
    public List<Card> Cards;

    internal Player(string name)
    {
        Name = name;
        Money = 420;
        Cards = new List<Card>();
    }

    public void GetCard(Card card)
    {
        Cards.Add(card);
    }

    public int CountCardsValue()
    {
//      TODO 
//          COUNT ZEO
        
        int totalCount = 0;
        for (int index = 0; index < Cards.Count; index++)
        {
            List<Card> countingCards = Cards;
            char value = countingCards[0].Name;
            
            if (Int32.TryParse(value.ToString(), out int result))
            {
                totalCount += result;
                countingCards.RemoveAt(0);
                break;
            }
            else
            {
                switch (value)
                {
                    case 'A':
                        
                        break;
                    case 'J':
                        totalCount += 11;
                        countingCards.RemoveAt(0);
                        break;
                    case 'Q':
                        totalCount += 12;
                        countingCards.RemoveAt(0);
                        break;
                    case 'K':
                        totalCount += 13;
                        countingCards.RemoveAt(0);
                        break;
                }
            }
        }

        return totalCount;
    }
}