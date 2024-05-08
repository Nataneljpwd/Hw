using Game;

public class Program
{
  public static void Main()
  {
    ConsoleGame game = new ConsoleGame();
    game.InitializeGame();
    game.StartGame();
    if (game.IsWin()) game.PrintWinMessage();
    else game.PrintLoseMessage();
  }
}
