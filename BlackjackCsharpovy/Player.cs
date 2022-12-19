using Newtonsoft.Json.Linq;
using System;
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
                    case "J":
                        totalCount += 11;
                        break;
                    case "Q":
                        totalCount += 12;
                        break;
                    case "K":
                        totalCount += 13;
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
}