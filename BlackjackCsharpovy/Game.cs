namespace BlackjackCsharpovy;

public class Game
{
    public bool Run;
    public Dealer Dealers;
    public Player Player;
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
                "Blackjack" +
                "[P]LAY" +
                "[L]EADER" +
                "[E]XIT"
            );
        }        
    }
}