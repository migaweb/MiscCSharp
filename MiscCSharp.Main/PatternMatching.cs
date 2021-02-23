using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscCSharp.Main
{
  public class PatternMatching
  {
    public PatternMatching()
    {
      IMyInterface instance = new MyClassyClass();

      if (!(instance is My2ndClassyClass)) { }
      if (instance is not My2ndClassyClass) { }

      if (instance is IMyInterface and not My2ndClassyClass) { }
      if (instance is IMyInterface and not My2ndClassyClass) { }

      if (instance is MyClassyClass or My2ndClassyClass) { }

      var myNumber = new Random().Next(1, 10);
      if (myNumber > 2 && myNumber < 8) { }
      if (myNumber is > 2 and < 8) { }

      switch (myNumber)
      {
        case >= 0 and <= 5:
          Console.WriteLine("More than 0, less than or equal to 5");
          break;
        case > 5 and <= 10:
          Console.WriteLine("More than 5, less than or equal to 10.");
          break;
      }

      var result = myNumber switch
      {
        >= 0 and <= 5 => "More than 0, less than or equal to 5",
        > 5 and <= 10 => "More than 5, less than or equal to 10."
      };
    }
  }

  public class My2ndClassyClass : IMyInterface
  {

  }

  public class MyClassyClass : IMyInterface
  {

  }

  public interface IMyInterface
  {

  }
}
