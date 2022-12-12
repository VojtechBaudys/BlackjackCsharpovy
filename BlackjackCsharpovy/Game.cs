using System.Text.RegularExpressions;

namespace BlackjackCsharpovy;

public class Game
{
    public bool Run;
    public Dealer Dealers;
    public Player Player;
    public Deck deck;
    internal Game()
    {
        Run = true;
        Dealers = new Dealer();
    }

    public void GameLoop()
    {
        while (Run)
        {
            Console.Write(
                "Blackjack\n" +
                "[P]LAY\n" +
                "[L]EADER\n" +
                "[E]XIT\n"
            );
            switch (GetInput())
            {
                case "p": case "play":
                    break;
                case "l": case "leader":
                    break;
                case "e": case "exit":
                    break;
            }
        }
    }
    
    private string GetInput(int maxLetters = 0, bool inLetters = true)
    {
        string input;
        {
            do
            {
                input = Console.ReadLine()!;
                input = input.ToLower();

                if (inLetters)
                {
                    if (Regex.IsMatch(input, @"^[a-zA-Z]+$"))
                    {
                        if (!(maxLetters >= input.Length) && maxLetters != 0)
                        {
                            Console.WriteLine("Enter only " + maxLetters + " letter");
                            input = "";
                        }
                        else if (input.Length == 0)
                        {
                            Console.WriteLine("Write something PLS");
                        }    
                    }
                    else
                    {
                        Console.WriteLine("Only letters PLS");
                        input = "";
                    }
                }
                else
                {
                    if (!(maxLetters >= input.Length) && maxLetters != 0)
                    {
                        Console.WriteLine("Enter only " + maxLetters + " letter");
                        input = "";
                    }
                    else if (input.Length == 0)
                    {
                        Console.WriteLine("Write something PLS");
                    }
                }
            } while (input == "");
			
            Console.Clear();
            return input.ToLower();
        }
    }
}