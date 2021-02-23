using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscCSharp.Main
{
  public class InitOnlyProp
  {
    public InitOnlyProp()
    {
      var temp = new InitOnlyProp
      {
        Name = "Test"
      };
      // This doesn't work
      //temp.Name = "Test2";

      Name = temp.Name;
    }

    public string Name { get; init; }
  }
}
