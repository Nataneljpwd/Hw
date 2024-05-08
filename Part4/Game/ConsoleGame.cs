using Enums;

namespace Game
{

  public class ConsoleGame
  {
    private Board GameBoard;
    private bool GameOver;
    private bool HasWon;

    public ConsoleGame()
    {
      this.GameBoard = new Board();
      this.GameOver = false;
      this.HasWon = false;
    }

    public void InitializeGame()
    {
      this.GameBoard.InitializeStartPos();
    }

    public void ResetGame()
    {
      this.GameBoard = new Board();
      this.GameOver = false;
    }

    internal void CheckGameStatus()
    {
      if (this.GameBoard.GetGameStatus() == GameStatus.Lose)
      {
        this.GameOver = true;
        this.HasWon = false;
      }
      else if (this.GameBoard.GetGameStatus() == GameStatus.Win)
      {
        this.GameOver = true;
        this.HasWon = true;
      }
    }

    public void StartGame()
    {
      // the gameloop
      while (!this.GameOver)
      {
        RedrawBoard();
        ConsoleKeyInfo key = Console.ReadKey(true);
        switch (key.Key)
        {
          case (ConsoleKey.UpArrow or ConsoleKey.W):
            this.GameBoard.Move(Direction.Up);
            break;
          case (ConsoleKey.DownArrow or ConsoleKey.S):
            this.GameBoard.Move(Direction.Down);
            break;
          case (ConsoleKey.LeftArrow or ConsoleKey.A):
            this.GameBoard.Move(Direction.Left);
            break;
          case (ConsoleKey.RightArrow or ConsoleKey.D):
            this.GameBoard.Move(Direction.Right);
            break;
          case (ConsoleKey.X):
            this.GameOver = true;
            Console.WriteLine("Goodbye!");
            break;
          case (ConsoleKey.R):
            this.ResetGame();
            this.InitializeGame();
            break;
          default:
            break;
        }
        CheckGameStatus();
      }
    }
    public bool IsWin()
    {
      return this.HasWon;
    }
    public void PrintWinMessage()
    {
      Console.WriteLine("Congratulations! You won!");
    }
    public void PrintLoseMessage()
    {
      Console.WriteLine("You Lost, Better Luck Next time");
    }
    internal void RedrawBoard()
    {
      Console.Clear();
      this.GameBoard.PrintBoardAndScore();
      Console.WriteLine("Press X to Exit or R to restart");
      Console.WriteLine("Press up, down, left or right arrow to move (WASD also works):");
    }

  }

}
