using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscCSharp.Main
{
  class SimpleTimer : IDisposable
  {
    private readonly Stopwatch _stopwatch;
    private readonly int _repeatCount = 15;

    public SimpleTimer()
    {
      _stopwatch = new Stopwatch();
      _stopwatch.Start();
      Console.WriteLine();
    }

    public void Dispose()
    {
      _stopwatch.Stop();
      var filler = new String('=', _repeatCount);
      Console.WriteLine($"{filler} {_stopwatch.ElapsedMilliseconds} ms {filler}");
    }
  }
}
