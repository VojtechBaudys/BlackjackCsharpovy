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
            Console.WriteLine(Cards[index].Name);
            // if (Int32.TryParse(Cards[index].Name, out int result))
            // {
            //     totalCount += result;
            // }
        }

        return totalCount;
    }
}