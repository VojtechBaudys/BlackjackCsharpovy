using System.Globalization;

namespace BlackjackCsharpovy;

public class Card
{
    public string Name;
    public int Value;
    public string Color;
    
    Card(string name, int value, string color)
    {
        Name = name;
        Value = value;
        Color = color;
    }
}