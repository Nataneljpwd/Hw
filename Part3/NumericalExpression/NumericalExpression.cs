namespace NumericalExpressionHW
{
  using System.Text;
  using System.Text.RegularExpressions;

  public class NumericalExpression
  {
    private long Number;
    /// The Function that Translates the numbers (0-19)
    private Func<long, String> NumberTranslator;
    ///Translates the tens (10-90)
    ///receives the number /10 -1
    private Func<long, String> NumberTensTranslator;
    ///Translates number powers (100, 1000, 1000000...)
    ///receives the log10 of the number
    private Func<long, String> NumberThouTranslator;
    private String ConnectorWord = "and";
    public NumericalExpression(long number)
    {
      this.Number = number;
      this.NumberTranslator = this.TranslateNumber;
      this.NumberTensTranslator = this.TranslateNubmerTens;
      this.NumberThouTranslator = this.TranslateNumberThou;
    }

    public void SetNumberTranslator(Func<long, String> translator)
    {
      this.NumberTranslator = translator;
    }

    public void SetNumberTensTranslator(Func<long, String> translator)
    {
      this.NumberTensTranslator = translator;
    }

    public void SetNumberThouTranslator(Func<long, String> translator)
    {
      this.NumberThouTranslator = translator;
    }
    public void SetConnectorWord(String word)
    {
      this.ConnectorWord = word;
    }

    public long GetValue()
    {
      return this.Number;
    }

    public int SumLetters(long num)
    {
      return this.NumberToWords(num).Length;
    }
    //would do static but assignment did not ask for it so it is non static
    //Overloading (having the same name with different params)
    public int SumLetters(NumericalExpression n)
    {
      return NumberToWords(n.GetValue()).Length;
    }

    public override String ToString()
    {
      return String.Join(" ", Regex.Matches(NumberToWords(this.Number), @"([A-Z][a-z]+)").Cast<Match>().Select(m => m.Value));
    }

    //might turn public so that is why there is an input
    private String NumberToWords(long num)
    {
      StringBuilder bldr = new StringBuilder();
      if (num < 0) bldr.Append("Minus");
      num = Math.Abs(num);
      List<int> digits = GetDigitsOfNumber(this.Number);

      //take the first numbers (up to 3) By getting the length and 
      int ind = 0;
      int len = digits.Count - (digits.Count / 3) * 3;
      while (digits.Count != 0)
      {

        //get the leftmost number of digits (either 1, 2 or 3)
        long curr = ConstructNumber(digits.Slice(0, ind == 0 ? len : 3));//only if it is the first condition will be met

        //we have either hundreds, tens or single
        if (curr < 20)
        {
          bldr.Append(this.TranslateNumber(curr));
        }
        else if (curr < 100)
        {
          bldr.Append(this.TranslateNubmerTens(curr / 10 - 1));// -1 because the zero index
          if (curr % 10 != 0)
          {
            bldr.Append(this.TranslateNumber(curr % 10));
          }
        }
        else
        {
          // hundreds
          bldr.Append(this.TranslateNumber(digits[0]));
          bldr.Append(this.TranslateNumberThou(2));// can hardcode because it is hundreds
          if (curr % 100 != 0)
          {
            int rem = (int)curr % 100;
            if (rem <= 10)
            {
              bldr.Append(this.ConnectorWord);
            }
            if (rem < 20)
            {
              bldr.Append(this.TranslateNumber(rem));
            }
            else
            {
              int rmndr = rem % 10;
              bldr.Append(this.TranslateNubmerTens(rem / 10 - 1));
              if (rmndr != 0)
              {
                bldr.Append(this.TranslateNumber(rmndr));
              }
            }
          }
        }
        int slc = ind == 0 ? len : 3;
        digits = digits.Slice(slc, digits.Count - slc);
        int pow = digits.Count / 3;
        pow *= 3;
        if (digits.Count / 3 != 0) bldr.Append(this.TranslateNumberThou(pow));
        ind += len;
      }


      return bldr.ToString();
    }

    internal long ConstructNumber(List<int> digits)
    {
      long a = 0;
      foreach (int d in digits)
      {
        a *= 10;
        a += d;
      }
      return a;
    }

    internal String TranslateNumber(long i)
    {
      if (i > 19) throw new InvalidDataException("Invalid number, The translateNumber function should take numbers from 0 to 19 inclusive");
      String[] ones = new String[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
      return ones[i];
    }

    internal String TranslateNubmerTens(long i)
    {
      String[] tens = new String[] { "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
      return tens[i];
    }
    ///i is the log10 of the number
    internal String TranslateNumberThou(long i)
    {

      Dictionary<long, String> map = new Dictionary<long, String>();
      map[2] = "Hundred";
      map[3] = "Thousand";
      map[6] = "Million";
      map[9] = "Billion";
      map[12] = "Trillion";
      return map[i];

    }

    internal List<int> GetDigitsOfNumber(long num)
    {
      char[] digits = num.ToString().ToCharArray();
      List<int> digitsList = new List<int>(8);
      foreach (char c in digits)
      {
        digitsList.Add(c - '0');
      }
      return digitsList;
    }

  }
}
