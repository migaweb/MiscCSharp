using Microsoft.EntityFrameworkCore;
using MiscCSharp.Data;
using MoreLinq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiscCSharp.Main
{
  class Program
  {
    static async Task Main(string[] args)
    {
      await Task.FromResult<string>("");
      new RecordTypes();
      //await AsyncAverage();
      //ParallelAverage();

      //CalculateCubesAsParallelOption();
      //CalculateCubesAsParallelOption(parallel: false);

      //ParallelEnumerableTest();

      //MergeOptionsParallel();

      //CustomAggregation();
      //MoreLinqBatch();
      //InterleaveDemo();
      //Permutations();
      //Split();
    }

    private static void Split()
    {
      var rand = new Random();
      var numbers = Enumerable.Range(1, 100).Select(_ => rand.Next(10));

      var split = numbers.Split(5);

      foreach (var group in split)
      {
        Console.WriteLine($"{group.Count()} elements: " + string.Join(", ", group));
      }
    }

    private static void Permutations()
    {
      var letters = "draw".ToCharArray();

      foreach (var item in letters.Permutations())
      {
        Console.WriteLine(new string(item.ToArray()));
      }
    }

    private static void InterleaveDemo()
    {
      var rand = new Random();
      var wholeNumbers = Enumerable.Range(1, 10).Select(_ => (double)rand.Next(10));
      var fractNumbers = Enumerable.Range(1, 10).Select(_ => rand.NextDouble());

      foreach (var d in wholeNumbers.Interleave(fractNumbers))
      {
        Console.Write($"{d}\t");
      }
      Console.WriteLine();
    }

    private static void MoreLinqBatch()
    {
      var numbers = Enumerable.Range(1, 100);
      foreach (var batch in numbers.Batch(10))
      {
        Console.WriteLine($"Got a batch");
        foreach (var i in batch)
          Console.Write($"{i}\t");
        Console.WriteLine("");
      }
    }

    private static void CustomAggregation()
    {
      var sum = Enumerable.Range(1, 1000).Sum();
      var agg = Enumerable.Range(1, 1000).Aggregate(0, (i, acc) => i + acc);
      var para = ParallelEnumerable.Range(1, 1000).Aggregate(
        0,
        (partialSum, i) => partialSum + i, // Func per task
        (total, subtotal) => total += subtotal, // Combine the results of all the tasks.
        i => i // Post processing
        );

      Console.WriteLine($"sum = {sum}");
      Console.WriteLine($"agg = {agg}");
      Console.WriteLine($"para = {para}");
    }

    private static void MergeOptionsParallel()
    {
      var numbers = Enumerable.Range(1, 50).ToArray();

      // Producer
      var results = numbers.AsParallel()
                           .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                           .Select(x =>
                           {
                             var result = Math.Log10(x);
                             Console.WriteLine($"Produced {result}");
                             return result;
                           });

      // Consumer
      foreach (var result in results)
      {
        Console.WriteLine($"Consumed {result}");
      }
    }

    static void ParallelEnumerableTest()
    {
      var cts = new CancellationTokenSource();
      var items = ParallelEnumerable.Range(1, 20);
      var results = items.WithCancellation(cts.Token).Select(i =>
      {
        double result = Math.Log10(i);

        //if (result > 1) throw new InvalidOperationException();

        Console.WriteLine($"i = {i}, tid = {Task.CurrentId}");
        return result;
      });

      try
      {
        foreach (var c in results)
        {
          if (c > 1) cts.Cancel();
          Console.WriteLine($"result = {c}");

        }
      }
      catch (AggregateException ae)
      {
        ae.Handle(e =>
        {
          Console.WriteLine($"{e.GetType().Name}: {e.Message}");
          return true;
        });
      }
      catch (OperationCanceledException oc)
      {
        Console.WriteLine($"{oc.GetType().Name}: {oc.Message}");
      }

      Console.Read();
    }

    static void CalculateCubesAsParallelOption(bool parallel = true)
    {
      Console.WriteLine($"Is parallel: {parallel}");

      using (new SimpleTimer())
      {
        const int count = 50;
        var items = Enumerable.Range(1, count).ToArray();

        var results = new int[count];

        if (parallel)
        {
          // AsOrdered
          items.AsParallel().ForAll(x =>
          {
            int newValue = x * x * x;
            Console.Write($"{newValue} ({Task.CurrentId})\t");
            results[x - 1] = newValue;
          });
        }
        else
        {
          items.ToList().ForEach(x =>
          {
            int newValue = x * x * x;
            Console.Write($"{newValue} ({Task.CurrentId})\t");
            results[x - 1] = newValue;
          });
        }
        Console.WriteLine();

        foreach (var i in results)
          Console.Write($"{i}\t");
        Console.WriteLine();
      }
    }

    static void ParallelAverage()
    {
      using (new SimpleTimer())
      using (var context = new DatabaseContext())
      using (var context2 = new DatabaseContext())
      {

        var customers = context.Products
                               .AverageAsync(p => p.UnitPrice)
                               .ContinueWith(t => Console.WriteLine($"Average unit price: {t.Result}"));
        var orders = context2.Orders
                            .GroupBy(o => o.OrderDate)
                            .AverageAsync(group => (double)group.Count())
                            .ContinueWith(t => Console.WriteLine($"Average orders per day is: {t.Result}"));

        Task.WaitAll(customers, orders);
      }
    }

    static async Task AsyncAverage()
    {
      using (new SimpleTimer())
      using (var context = new DatabaseContext())
      {
        var customers = await context.Products
                               .AverageAsync(p => p.UnitPrice);
        //.ContinueWith(t => Console.WriteLine($"Average unit price: {t.Result}"));
        var orders = await context.Orders
                            .GroupBy(o => o.OrderDate)
                            .AverageAsync(group => (double)group.Count());
        //.ContinueWith(t => Console.WriteLine($"Average orders per day is: {t.Result}"));

        Console.WriteLine($"Average unit price: {customers}");
        Console.WriteLine($"Average orders per day is: {orders}");
      }
    }
  }
}
