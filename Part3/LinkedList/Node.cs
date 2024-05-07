namespace LinkedListHW
{
  public class Node
  {

    //possible to also jsut use public modifier instead of getters and setters
    private int Value;
    private Node? Next;

    public Node(int value)
    {
      this.Value = value;
    }

    public int GetValue()
    {
      return this.Value;
    }

    public void SetValue(int value)
    {
      this.Value = value;
    }

    public Node? GetNext()
    {
      return this.Next;
    }

    public void SetNext(Node? next)
    {
      this.Next = next;
    }

    public override String ToString()
    {
      return String.Format("val:{0}, next:{1}", this.Value, this.Next == null ? null : this.Next.Value);
    }
  }
}
