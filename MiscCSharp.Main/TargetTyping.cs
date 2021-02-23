using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscCSharp.Main
{
  public class TargetTyping
  {
    public TargetTyping()
    {
      MyClass instance = new ();
      MyClass instance2 = new() { 
        MyValue = ""
      };

      MyClassWithConstructor inst = new("test"); 
    }
  }

  public class MyClass
  {
    public string MyValue { get; set; }
  }

  public class MyClassWithConstructor
  {
    private readonly string _myValue;

    public MyClassWithConstructor(string myValue)
    {
      _myValue = myValue;
    }
  }
}
