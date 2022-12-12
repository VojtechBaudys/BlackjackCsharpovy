using System.Globalization;

namespace BlackjackCsharpovy;

public class Card
{
    public char Name;
    public char Symbol;
    
    internal Card(char name, char symbol)
    {
        Name = name;
        Symbol = symbol;
    }
}