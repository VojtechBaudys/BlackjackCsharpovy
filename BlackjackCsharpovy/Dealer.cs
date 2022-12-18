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
}