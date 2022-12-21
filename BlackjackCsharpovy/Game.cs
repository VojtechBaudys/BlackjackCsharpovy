using System.Collections.Concurrent;
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
                "\nBLACKJACK\n" +
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
        Console.WriteLine(string.Join("\n", sortedDict.Select(pair => $"{pair.Key}: {pair.Value}\n")));
    }

//  TODO
//      LOAD MONEY
//      SAVE TO LEADER
    
    public void Play()
    {
        Player = new Player(GetUserName());
        Dealer = new Dealer();
        Deck = new Deck(4);
        int bet = -1;
        bool play = true;

        while (play)
        {
            bool round = true;
            Player.Cards.Clear();
            Dealer.Cards.Clear();
            
            while (round)
            {
                Console.WriteLine("CREDIT: " + Player.Money + "\nSET YOUR BET");
                int input = Int32.Parse(GetInput(0, "number"));
                if (input > 0 && input <= Player.Money)
                {
                    bet = input;
                    break;
                }
                else
                {
                    Console.WriteLine("\nENTER VALID NUMBER");
                }
            }
            
            while (round)
            {
                bool pick = true;
                
                Player.GetCard(Deck.DealCard());
                Player.GetCard(Deck.DealCard());
                Dealer.GetCard(Deck.DealCard());

                while (Player.CountCardsValue() < 21 && pick)
                {
                    GetStats();
                    Console.Write(
                        "\nSELECT ONE OPTION\n" +
                        "[H]IT\n" +
                        "[D]OUBLE\n" +
                        "[S]TAND\n"
                    );
                    switch (GetInput())
                    {
                        case "h": case "hit":
                            Player.GetCard(Deck.DealCard());
                            break;
                        case "d": case "double":
                            if (bet * 2 <= Player.Money)
                            {
                                Player.GetCard(Deck.DealCard());
                                bet *= 2;
                                pick = false;
                            }
                            break;
                        case "s": case "stand":
                            pick = false;
                            break;
                    }
                }
                
                while (Dealer.CountCardsValue() < 17 && Player.CountCardsValue() <= 21)
                {
                    GetStats();
                    Dealer.GetCard(Deck.DealCard());
                }

                string win;
                if (Player.CountCardsValue() > 21)
                {
                    win = "dealer";
                }
                else if (Dealer.CountCardsValue() > 21)
                {
                    win = "player";
                }
                else if (Player.CountCardsValue() > Dealer.CountCardsValue())
                {
                    win = "player";
                }
                else if (Player.CountCardsValue() < Dealer.CountCardsValue())
                {
                    win = "dealer";
                }
                else
                {
                    win = "draft";
                }
                
                switch (win)
                {
                    case "player":
                        GetStats();
                        Console.WriteLine("\nWIN");
                        Player.Money += bet;    
                        break;
                    case "dealer":
                        GetStats();
                        Console.WriteLine("\nLOSE");
                        Player.Money -= bet;
                        break;
                    case "draft":
                        GetStats();
                        Console.WriteLine("\nDRAFT");
                        break;
                }
                
                Console.ReadLine();
                Console.Clear();

                bool output = true; 
                while (output)
                {
                    Console.WriteLine(
                        "CREDIT: " + Player.Money + "\n\n" +
                        "ANOTHER ROUND?\n" +
                        "[Y]ES\n" +
                        "[N]O"
                    );
                    switch (GetInput())
                    {
                        case "y": case "yes":
                            output = false;
                            round = false;
                            break;
                        case "n": case "no":
                            output = false;
                            play = false;
                            round = false;
                            break;
                    }
                }
            }
        }
    }

    private void GetStats()
    {
        Console.Clear();
        Console.WriteLine("\nPlayer");
        Player.PrintCards();
        Console.WriteLine("\n" + Player.CountCardsValue());
        Console.WriteLine("\nDealer");
        Dealer.PrintCards();
        if (Dealer.CountCardsValue() != 0)
        {
            Console.WriteLine("\n" + Dealer.CountCardsValue());
        }
    }
    
    private string GetUserName()
    {
        string input = "";
        while (input == "")
        {
            Console.Write(
                "\nENTER YOUR USERNAME\n"
            );
            input = GetInput();
        }
        return input;
    }
    
    private string GetInput(int maxLetters = 0, string type = "letter")
    {
        string input;
        {
            do
            {
                input = Console.ReadLine()!;
                input = input.ToLower();

                if (type == "letter")
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
                else if (type == "number")
                {
                    if (Regex.IsMatch(input, @"^[1234567890]+$"))
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
                        Console.WriteLine("Only numbers PLS");
                        input = "";
                    }
                }
            } while (input == "");
			
            Console.Clear();
            return input.ToLower();
        }
    }
}