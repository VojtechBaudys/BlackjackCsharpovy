using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace BlackjackCsharpovy;

public class Game
{
    //  ATTRIBUTES
    public bool Run;
    public Dealer Dealer;
    public Player Player;
    public Deck Deck;
    public MenuController MenuController;
    
    internal Game()
    {
        Player = new Player("");
        MenuController = new MenuController();
        Dealer = new Dealer();
        Run = true;
    }

    //  GAMELOOP    
    public void GameLoop()
    {
        while (Run)
        {
            MenuController.MainMenu();
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
        Player.StatsExist();
        Console.Write("LEADER\n\n");
        
        //  LOAD FILE        
        string jsonString = File.ReadAllText("stats.json");
        Dictionary<string, int> jsonFile = JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonString);
        
        //  PRINT DATA
        var sortedDict = (from entry in jsonFile orderby entry.Value descending select entry).Take(5);
        Console.WriteLine(string.Join("\n", sortedDict.Select(pair => $"{pair.Key}: {pair.Value}\n")));
    }

    public void Play()
    {
        Player = new Player(GetUserName());
        Deck = new Deck(4);
        int bet = -1;
        bool play = true;

        while (play)
        {
            bool round = true;
            Player.Cards.Clear();
            Dealer.Cards.Clear();

            bet = GetBet(round);
            
            while (round)
            {
                bool pick = true;
                
                Player.GetCard(Deck.DealCard());
                Player.GetCard(Deck.DealCard());
                Dealer.GetCard(Deck.DealCard());

                // HITTING LOOP
                while (Player.CountCardsValue() < 21 && pick)
                {
                    GetStats();
                    MenuController.OptionMenu();
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

                // RESULT
                switch (GetResult())
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
                
                // SAVE DATA TO JSONEK
                Player.SaveStats();
                Console.ReadLine();
                Console.Clear();

                bool output;
                if (Player.Money == 0)
                {
                    output = false;
                    play = false;
                    round = false;
                }
                else
                {
                    output = true;
                }
                
                // ANOTHER ROUND LOOP
                while (output)
                {
                    MenuController.AnotherRoundMenu(Player.Money);
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
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                }
            }
        }
    }

    private string GetResult()
    {
        if (Player.CountCardsValue() > 21)
        {
            return "dealer";
        }
        else if (Dealer.CountCardsValue() > 21)
        {
            return "player";
        }
        else if (Player.CountCardsValue() > Dealer.CountCardsValue())
        {
            return "player";
        }
        else if (Player.CountCardsValue() < Dealer.CountCardsValue())
        {
            return "dealer";
        }
        else
        {
            return "draft";
        }
    }
    
    private int GetBet(bool round)
    {
        int bet = -1;
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

        return bet;
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
    
    //  maxLetters [int]
    //      value:  0 = default (none)
    //  type [string]
    //      value:  "letter" (char/string)
    //              "number" (int/numeric value)
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