namespace BlackjackCsharpovy;

public class Dealer
{
    public List<Card> Cards;

    internal Dealer()
    {
        Cards = new List<Card>();
    }
    
    public void GetCard(Card card)
    {
        Cards.Add(card);
    }

    public int CountCardsValue()
    {
        int totalCount = 0;
        List<Card> a = new List<Card>();

        for (int index = 0; index < Cards.Count; index++)
        {
            if (Int32.TryParse(Cards[index].Name, out int result))
            {
                totalCount += result;
            }
            else
            {
                switch (Cards[index].Name)
                {
                    case "A":
                        a.Add(Cards[index]);
                        break;
                    case "J": case "Q": case "K":
                        totalCount += 10;
                        break;
                }
            }
        }

        for (int index = 0; index < a.Count(); index++)
        {
            if (totalCount + 11 <= 21)
            {
                totalCount += 11;
            }
            else
            {
                totalCount++;
            }
        }

        return totalCount;
    }
    
    public void PrintCards()
    {
        for (int index = 0; index < Cards.Count; index++)
        {
            Console.Write(Cards[index].Name + Cards[index].Symbol + " ");
        }
    }
}