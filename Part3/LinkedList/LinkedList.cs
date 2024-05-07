using System.Text;

namespace LinkedListHW
{
  public class LinkedList
  {

    private Node? Head;
    private Node? Tail;
    private Node? Max;
    private Node? Min;
    public LinkedList() { }

    public void Append(int val)
    {
      Node nn = new Node(val);
      UpdateMinMax(nn);
      if (this.Head == null)
      {
        this.Head = nn;
        this.Tail = nn;
      }
      else
      {
        if (this.Tail == null) throw new NullReferenceException("Tail was null");// should never happen
        this.Tail.SetNext(nn);
        this.Tail = nn;
      }
    }
    public void Prepend(int val)
    {
      Node nn = new Node(val);
      UpdateMinMax(nn);
      if (this.Max == null || this.Max.GetValue() < val) this.Max = nn;
      nn.SetNext(this.Head);
      this.Head = nn;
    }

    public int Pop()
    {
      if (this.Head == null)
      {
        throw new IndexOutOfRangeException("Tried to pop on an empty list");// in case the list is empty
      }
      Node n = this.Head;
      if (n.GetNext() == null)
      {
        this.Head = null;
        this.Tail = null;
        return n.GetValue();
      }
      while (n.GetNext() != this.Tail)
      {
        n = n.GetNext();
      }
      if (n != null)
      {
        n.SetNext(null);
      }
      int ret = this.Tail.GetValue();
      this.Tail = n;
      UpdateMinMax();
      return ret;
    }

    public int Unqueue()
    {
      int val = this.Head.GetValue();
      this.Head = this.Head.GetNext();
      UpdateMinMax();
      return val;
    }

    public IEnumerable<int> ToList()
    {
      Node n = this.Head;
      while (n != null)
      {
        yield return n.GetValue();
        n = n.GetNext();
      }
    }

    public bool IsCircular()
    {

      if (this.Head == null || this.Head.GetNext() == null) return false;

      Node tortoise = this.Head, hare = this.Head;

      while (hare != null && hare.GetNext() != null)
      {
        tortoise = tortoise.GetNext();
        hare = hare.GetNext().GetNext();
        if (tortoise == hare) return true;
      }

      return false;
    }

    public void Sort()
    {
      //two approaches, either just use an array, sort the array and recostruct or implement bubble sort on the linkedlist
      /*approach 1
       * if(this.head==null)return;
       *  int size = 0;
       *  Node n = this.head;
       *  while(n != null){
       *    n = n.GetNext();
       *    size++;
       *  }
       *  int[] toSort = new int[size];
       *  n = this.head;
       *  for(int i=0;i<size;i++){
       *    toSort[i] = n.GetValue();
       *    n = n.GetNext();
       *  }
       *  Array.sort(toSort);
       *  //reconstruct the list
       *  this.head = new Node(toSort(0));
       *  n = this.head;
       *  for(int i = 1;i<size;i++){
       *    Node tmp = new Node(toSort[i]);
       *    n.SetNext(tmp);
       *    n = tmp;
       *  }
       * */
      //approach 2
      bool swapped;
      Node current;

      if (this.Head == null)
        return;

      do
      {
        swapped = false;
        current = this.Head;

        while (current.GetNext() != null)
        {
          if (current.GetValue() > current.GetNext().GetValue())
          {
            Swap(current, current.GetNext());
            swapped = true;
          }
          current = current.GetNext();
        }
      } while (swapped);
      UpdateMinMax();
    }

    public Node GetMaxNode()
    {
      return this.Max;
    }
    public Node GetMinNode()
    {
      return this.Min;
    }

    internal void Swap(Node swap1, Node swap2)// if it was allowed to implement a doubly linked node and list, this would have been an o(1) operation in addition to a few more funcs like Pop
    {
      int tmp = swap2.GetValue();
      swap2.SetValue(swap1.GetValue());
      swap1.SetValue(tmp);
    }

    internal void UpdateMax()
    {

      Node n = this.Head;
      this.Max = this.Head;
      while (n != null)
      {
        if (this.Max == null || n.GetValue() > this.Max.GetValue())
        {
          this.Max = n;
        }
        n = n.GetNext();
      }
    }
    internal void UpdateMin()
    {
      Node n = this.Head;
      this.Min = this.Head;
      while (n != null)
      {
        if (this.Min == null || n.GetValue() < this.Min.GetValue())
        {
          this.Min = n;
        }
        n = n.GetNext();
      }
    }

    internal void UpdateMinMax()
    {
      UpdateMin();
      UpdateMax();
    }

    internal void UpdateMinMax(Node n)
    {
      if (this.Max == null || this.Max.GetValue() < n.GetValue()) this.Max = n;
      if (this.Min == null || this.Min.GetValue() > n.GetValue()) this.Min = n;
    }

    public Node Get(int ind)
    {
      Node n = this.Head;
      while (ind > 0 && n != null)
      {
        n = n.GetNext();
        ind--;
      }
      return n;
    }

    public override String ToString()
    {
      StringBuilder bldr = new StringBuilder();
      foreach (int n in this.ToList())
      {
        bldr.Append(String.Format("{0} -> ", n));
      }
      bldr.Append("\n");
      bldr.Append(String.Format("Max:{0}, Min:{1}", this.Max, this.Min));
      return bldr.ToString();
    }
  }
}
