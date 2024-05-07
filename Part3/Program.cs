using System.Text;
using LinkedListHW;
using NumericalExpressionHW;

public class Run
{
  public static void Main()
  {
    PrintWithStars("ToList is used in the ToString so no tests here");

    PrintWithStars("LinkedList");

    PrintWithStars("Append and Prepend");

    Random r = new Random();

    LinkedList lst = new LinkedList();
    for (int i = 0; i < 50; i++)
    {
      lst.Append(r.Next(100));
    }

    PrintWithStars("Append");
    PrintResult(lst.ToString());
    for (int i = 0; i < 10; i++)
    {
      lst.Prepend(r.Next(-100, 0));
    }

    PrintWithStars("Prepend");

    PrintResult(lst.ToString());

    PrintWithStars("Pop and Unqueue");

    PrintWithStars("Pop 5 elements");

    for (int i = 0; i < 5; i++)
    {
      lst.Pop();
    }
    PrintResult(lst.ToString());
    PrintWithStars("Unqueue 5 elements");
    for (int i = 0; i < 5; i++)
    {
      lst.Unqueue();
    }
    PrintResult(lst.ToString());

    PrintWithStars("Sort");
    lst.Sort();
    PrintResult(lst.ToString());

    PrintWithStars("Is Circular");

    LinkedList circular = new LinkedList();
    int len = 25;
    for (int i = 0; i < len; i++)
    {
      circular.Append(r.Next(100));
    }
    PrintResult("Creating loop");
    Node n = circular.Get(len - 1);
    Node n2 = circular.Get(len / 2);
    n.SetNext(n2);
    PrintResult(circular.IsCircular().ToString());

    PrintResult("Not Circular");
    PrintResult(lst.IsCircular().ToString());

    PrintWithStars("Get Max Node and Min Node on the Non Circular List");
    PrintResult(lst.ToString());
    PrintResult(String.Format("Max:{0}, Min:{1}", lst.GetMaxNode(), lst.GetMinNode()));
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
    PrintWithStars("Numerical Expression");
    NumericalExpression exp = new NumericalExpression(25_623_366);
    PrintResult(exp.ToString());
    NumericalExpression exp2 = new NumericalExpression(25_623_366_816_233);
    PrintResult(exp2.ToString());

  }
  private static void PrintWithStars(String toPrint)
  {
    StringBuilder bldr = new StringBuilder();
    bldr.Append("**");
    foreach (char c in toPrint)
    {
      bldr.Append("*");
    }
    bldr.Append("**");
    Console.WriteLine(bldr.ToString());
    Console.WriteLine(String.Format("* {0} *", toPrint));
    Console.WriteLine(bldr.ToString());
    Console.WriteLine();
    Console.WriteLine();
  }
  private static void PrintResult(String print)
  {
    Console.WriteLine(print);
    Console.WriteLine();
    Console.WriteLine();
  }
}

