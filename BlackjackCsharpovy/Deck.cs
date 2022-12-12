using System.Collections;
using System.Diagnostics;

namespace BlackjackCsharpovy;

public class Deck
{
    public List<Card> Cards;

    internal Deck()
    {
        List<char> symbols = new List<char>()
        {
            '♣', '♦', '♥', '♠'
        };
        for (int value = 0; value < 13; value++)
        {
            for (int symbol = 0; symbol < 4; symbol++)
            {
                char name;
                switch (value)
                {
                    case 0:
                        name = 'A';
                        break;
                    case 11:
                        name = 'J';
                        break;
                    case 12:
                        name = 'Q';
                        break;
                    case 13:
                        name = 'K';
                        break;
                    default:
                        name = (char) value;
                        break;
                }
                Cards.Add(new Card(name, symbols[symbol]));
            }
        }
    }

    void Shuffle()
    {
        
    }
}