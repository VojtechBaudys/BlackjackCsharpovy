namespace BlackjackCsharpovy;

public class MenuController
{
	//	MENU STORAGE

	public void MainMenu()
	{
		Console.Write(
			"\nBLACKJACK\n" +
			"[P]LAY\n" +
			"[L]EADER\n" +
			"[E]XIT\n"
		);
	}

	public void OptionMenu()
	{
		Console.Write(
			"\nSELECT ONE OPTION\n" +
			"[H]IT\n" +
			"[D]OUBLE\n" +
			"[S]TAND\n"
		);
	}

	public void AnotherRoundMenu(int money)
	{
		Console.WriteLine(
			"CREDIT: " + money + "\n\n" +
			"ANOTHER ROUND?\n" +
			"[Y]ES\n" +
			"[N]O"
		);
	}
}