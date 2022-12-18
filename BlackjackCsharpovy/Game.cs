using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace BlackjackCsharpovy;

public class Game
{
    public bool Run;
    public Dealer Dealer;
    public Player Player;
    public Deck Deck;
    internal Game()
    {
        Run = true;
    }

    public void GameLoop()
    {
        while (Run)
        {
            Console.Write(
                "BLACKJACK\n" +
                "[P]LAY\n" +
                "[L]EADER\n" +
                "[E]XIT\n"
            );
            switch (GetInput())
            {
                case "p": case "play":
                    Play();
                    break;
                case "l": case "leader":
                    LeaderBoard();
                    break;
                case "e": case "exit":
                    Run = false;
                    break;
            }
        }
    }

    public void LeaderBoard()
    {
        Console.Write("LEADER\n\n");
        
        string jsonString = File.ReadAllText("stats.json");
        Dictionary<string, int> jsonFile = JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonString);

        var sortedDict = (from entry in jsonFile orderby entry.Value descending select entry).Take(5);
        Console.WriteLine(string.Join("\n", sortedDict.Select(pair => $"{pair.Key}: {pair.Value}")));
        Console.WriteLine();
    }

    public void Play()
    {
        Player = new Player(GetUserName());
        Dealer = new Dealer();
        Deck = new Deck(4);
        while (Run)
        {
            Player.GetCard(Deck.DealCard());
            Dealer.GetCard(Deck.DealCard());

            Player.CountCardsValue();
        }
    }
    
    private string GetUserName()
    {
        string input = "";
        while (input == "")
        {
            Console.Write(
                "ENTER YOUR USERNAME\n"
            );
            input = GetInput();
        }
        return input;
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