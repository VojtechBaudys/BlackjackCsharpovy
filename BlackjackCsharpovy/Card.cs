using System.Globalization;

namespace BlackjackCsharpovy;

public class Card
{
    public string Name;
    public char Symbol;
    
    internal Card(string name, char symbol)
    {
        Name = name;
        Symbol = symbol;
    }
}