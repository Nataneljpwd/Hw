using System.Text;

namespace LinkedListHW
{
  public class LinkedList
  {

    private Node? Head;
    private Node? Tail;
    private Node? MaxNode;
    private Node? MinNode;
    public LinkedList() { }

    public void Append(int val)
    {
      Node nodeToAppend = new Node(val);

      UpdateMinMaxNodes(nodeToAppend);

      if (this.Head == null)
      {
        this.Head = nodeToAppend;
        this.Tail = nodeToAppend;
      }
      else
      {
        if (this.Tail == null) throw new NullReferenceException("Tail was null");// should never happen
        this.Tail.SetNext(nodeToAppend);
        this.Tail = nodeToAppend;
      }
    }
    public void Prepend(int val)
    {
      Node nodeToPrepend = new Node(val);

      UpdateMinMaxNodes(nodeToPrepend);

      if (this.MaxNode == null || this.MaxNode.GetValue() < val) this.MaxNode = nodeToPrepend;

      nodeToPrepend.SetNext(this.Head);
      this.Head = nodeToPrepend;
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

      UpdateMinMaxNodes();

      return ret;
    }

    public int Unqueue()
    {
      int val = this.Head.GetValue();
      this.Head = this.Head.GetNext();

      UpdateMinMaxNodes();

      return val;
    }

    public IEnumerable<int> ToList()
    {
      Node iterateor = this.Head;

      while (iterateor != null)
      {
        yield return iterateor.GetValue();

        iterateor = iterateor.GetNext();
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

      UpdateMinMaxNodes();
    }

    public Node GetMaxNode()
    {
      return this.MaxNode;
    }
    public Node GetMinNode()
    {
      return this.MinNode;
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
      this.MaxNode = this.Head;
      while (n != null)
      {
        if (this.MaxNode == null || n.GetValue() > this.MaxNode.GetValue())
        {
          this.MaxNode = n;
        }
        n = n.GetNext();
      }
    }
    internal void UpdateMin()
    {
      Node n = this.Head;
      this.MinNode = this.Head;
      while (n != null)
      {
        if (this.MinNode == null || n.GetValue() < this.MinNode.GetValue())
        {
          this.MinNode = n;
        }
        n = n.GetNext();
      }
    }

    internal void UpdateMinMaxNodes()
    {
      UpdateMin();
      UpdateMax();
    }

    internal void UpdateMinMaxNodes(Node n)
    {
      if (this.MaxNode == null || this.MaxNode.GetValue() < n.GetValue()) this.MaxNode = n;
      if (this.MinNode == null || this.MinNode.GetValue() > n.GetValue()) this.MinNode = n;
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
      bldr.Append(String.Format("Max:{0}, Min:{1}", this.MaxNode, this.MinNode));

      return bldr.ToString();
    }
  }
}
