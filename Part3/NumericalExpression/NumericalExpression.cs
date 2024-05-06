namespace NumericalExpression
{
  using System.Text;
  public class NumericalExpression
  {
    private long Number;
    /// The Function that Translates the numbers (0-9)
    /// Input is a single Digit and output is the name of the digit 
    private Func<int, String> NumberTranslator;
    /// The function that Translates the Number Size (Hundred, Thousand, Million, Billion...)
    /// Recieves the Size of the Number and returns the Number size annotation
    private Func<int, String> NumberNameTranslator;
    private bool Minus;
    public NumericalExpression(long number)
    {
      if (number < 0) this.Minus = true;
      this.Number = Math.Abs(number);
    }

    public void SetNumberTranslator(Func<int, String> translator)
    {
      this.NumberTranslator = translator;
    }

    public void SetNumberNameTranslator(Func<int, String> translator)
    {
      this.NumberNameTranslator = translator;
    }

    public String ToString()
    {
      StringBuilder bldr = new StringBuilder();
      List<int> digits = GetDigitsOfNumber(this.Number);


      return bldr.ToString();
    }

    internal String TranslateNumber(int i)
    {
      if (i > 12) throw new InvalidDataException("Invalid number, The translateNumber function should take numbers from 0 to 12 inclusive");
      String[] map = new String[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve" };
      return map[i];
    }

    ///@param i is the digit count of the number name
    internal String TranslateNubmerName(int i)
    {
      if (i == -1) return "teen";
      String[] map = new String[] { "Ten", "Hundred", "Thousand", "Million", "Billion", "Trillion" };
      return "";
    }

    internal List<int> GetDigitsOfNumber(long num)
    {
      char[] digits = num.ToString().ToCharArray();
      List<int> digitsList = new List<int>();
      foreach (char c in digits)
      {
        digitsList.Append(c - '0');
      }
      return digitsList;
    }

  }
}
