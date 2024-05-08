using Enums;

namespace Game
{
  public class Board
  {

    private const int SIZE = 4;
    private int[,] GameBoard;// 0 is empty
    private UInt16 EmptyCells;// 0 if empty, 1 if occupied
    private int Points;

    public Board()
    {
      this.GameBoard = new int[SIZE, SIZE];
      this.EmptyCells = UInt16.MaxValue;
      this.Points = 0;
    }
    public int[,] GetBoard()
    {
      return this.GameBoard;
    }
    protected void SetBoard(int[,] b)
    {
      this.GameBoard = b;
    }
    public void InitializeStartPos()
    {
      CreateCellAtRandomPlace();
      CreateCellAtRandomPlace();
    }

    public void Move(Direction dir)
    {
      //move once to the desired side
      if (dir == Direction.Up || dir == Direction.Down) MoveVertically(dir);
      else MoveHorizontal(dir);
      //merge in the Dirction of the move
      int score = Merge(dir);
      //move again to the same side
      if (dir == Direction.Up || dir == Direction.Down) MoveVertically(dir);
      else MoveHorizontal(dir);
      //create a random cell somewhere
      CreateCellAtRandomPlace();

      this.Points += score;
    }

    private int GetCell(int[] pos)
    {
      return this.GameBoard[pos[0], pos[1]];
    }

    private void SetCell(int[] pos, int val)
    {
      if (GetCell(pos) != 0)
        SetCellOccupied(pos);
      else
        SetCellEmpty(pos);
      this.GameBoard[pos[0], pos[1]] = val;
    }
    private int Merge(Direction dir)
    {
      if (dir == Direction.Up || dir == Direction.Down) return MergeVertical(dir);
      else return MergeHorizontal(dir);
    }

    internal int MergeVertical(Direction dir)
    {
      int inc = dir == Direction.Up ? 1 : -1;
      int res = 0;

      for (int col = 0; col < SIZE; col++)
      {
        for (int row = (inc == 1 ? 0 : SIZE - 1); row <= SIZE - 1 && row >= 0; row += inc)
        {
          int[] pos1 = new int[] { row, col }, pos2 = new int[] { row + inc, col };
          if (GetCell(pos1) == GetCell(pos2))
          {
            // get the one that is closer to the side of the direction
            if (dir == Direction.Up)
            {
              int score = 2 * GetCell(pos1);
              this.SetCell(new int[] { Math.Min(pos1[0], pos2[0]), col }, score);
              res += score;
              this.SetCell(new int[] { Math.Max(pos1[0], pos2[0]), col }, 0);
            }
            else
            {
              int score = 2 * GetCell(pos1);
              this.SetCell(new int[] { Math.Max(pos1[0], pos2[0]), col }, score);
              res += score;
              this.SetCell(new int[] { Math.Min(pos1[0], pos2[0]), col }, 0);
            }
          }
        }
      }
      return res;
    }
    internal int MergeHorizontal(Direction dir)
    {

      int inc = dir == Direction.Right ? -1 : 1;// we go from the opposite direction
      int res = 0;

      for (int row = 0; row < SIZE; row++)
      {
        for (int col = (inc == -1 ? SIZE - 1 : 0); col < SIZE && col >= 0; col += inc)
        {

          int[] pos1 = new int[] { row, col + inc }, pos2 = new int[] { row, col + inc };
          if (GetCell(pos1) == GetCell(pos2))
          {
            if (dir == Direction.Right)
            {
              int score = 2 * GetCell(pos1);
              this.SetCell(new int[] { row, Math.Max(pos1[1], pos2[1]) }, score);
              res += score;
              this.SetCell(new int[] { row, Math.Min(pos1[1], pos2[1]) }, 0);
            }
            else
            {
              int score = 2 * GetCell(pos1);
              this.SetCell(new int[] { row, Math.Min(pos1[1], pos2[1]) }, score);
              res += score;
              this.SetCell(new int[] { row, Math.Max(pos1[1], pos2[1]) }, 0);
            }
          }

        }
      }
      return res;
    }

    internal void SetCellOccupied(int[] pos)
    {
      this.EmptyCells = (UInt16)(this.EmptyCells | ConvertPosToUint(pos));// changes the specific int to 1
    }
    ///True if Occupied, else false
    internal bool IsCellOccupied(int[] pos)
    {
      return (ConvertPosToUint(pos) & this.EmptyCells) > 0;
    }
    internal UInt16 ConvertPosToUint(int[] pos)
    {
      if (pos[0] < 0 || pos[1] < 0) return 0;
      UInt16 res = (UInt16)((1) << (UInt16)TwoDimPosToSingleInt(pos));
      return res;
    }
    internal int TwoDimPosToSingleInt(int[] pos)
    {
      return pos[0] * SIZE + pos[1];
    }
    // would convert to an apply func but cant gen enough parameters and dont want to use arrays and stuff for that
    internal void MoveVertically(Direction dir)
    {
      int inc = dir == Direction.Up ? 1 : -1;

      for (int col = 0; col < SIZE; col++)
      {
        for (int row = (inc == 1 ? 0 : SIZE - 1); row <= SIZE - 1 && row >= 0; row += inc)
        {
          if (GetCell(new int[] { row, col }) == 0)
            Swap(new int[] { row + inc, col }, new int[] { row, col });
        }
      }
    }
    internal void MoveHorizontal(Direction dir)
    {

      int inc = dir == Direction.Right ? -1 : 1;// we go from the opposite direction

      for (int row = 0; row < SIZE; row++)
      {
        for (int col = (inc == -1 ? SIZE - 1 : 0); col < SIZE && col >= 0; col += inc)
        {
          if (GetCell(new int[] { row, col }) == 0)
            Swap(new int[] { row, col + inc }, new int[] { row, col });
        }
      }
    }

    internal void Swap(int[] pos1, int[] pos2)
    {
      int tmp = this.GetCell(pos1);
      this.SetCell(pos1, this.GetCell(pos2));
      this.SetCell(pos2, tmp);
    }

    internal void SetCellEmpty(int[] pos)
    {
      this.EmptyCells &= (UInt16)~(1 << TwoDimPosToSingleInt(pos));// changes the specific bit to 0
    }

    internal int[] UInt16ToPosArr(UInt16 pos)
    {
      return new int[] { pos / SIZE, pos % SIZE };
    }

    internal void CreateCellAtRandomPlace()
    {
      Random rnd = new Random();
      int sc = rnd.NextDouble() > 0.5 ? 2 : 4;
      //get random non occupied Cell
      if (this.EmptyCells == UInt16.MaxValue) return;// we cannot create any new cell but the game is not over
      UInt16 empty = (UInt16)(1 << rnd.Next(SIZE * SIZE));
      while (IsCellOccupied(UInt16ToPosArr(empty)))// an O(1) operation beause worst case is 15 iterations
      {
        empty = (UInt16)(empty << 1);
      }
      this.SetCell(UInt16ToPosArr(empty), sc);
    }

    // internal bool IsRowFull(int row)
    // {
    //   for (int col = 0; col < SIZE; col++)
    //   {
    //     if (!IsCellOccupied(new int[] { row, col }))
    //     {
    //       return false;
    //     }
    //   }
    //   return true;
    // }

    // internal bool IsColFull(int col)
    // {
    //   for (int row = 0; row < SIZE; row++)
    //   {
    //     if (!IsCellOccupied(new int[] { row, col }))
    //     {
    //       return false;
    //     }
    //   }
    //   return true;
    // }

  }
}