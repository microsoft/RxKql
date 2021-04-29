using System;
using System.Collections.Generic;
using System.Reactive.Kql;
using System.Reactive.Kql.CustomTypes;
using System.Reactive.Linq;
using System.Threading;

namespace RxKqlNodeSample
{
    class Program
    {
        static Random _random = new Random();
        static string[] _symbols = { "MSFT", "AAPL", "AMZN" };

        static void Main()
        {
            // Generate infinite, real-time stream of stock quotes
            IObservable<IDictionary<string, object>> quotes = StockQuotes().ToObservable();

            // Evaluate multiple queries on the stream
            KqlNodeHub hub = KqlNodeHub.FromFiles(
                quotes, PrintOutput, "quotes", "MsftQueries.kql", "AnySymbolQueries.kql");

            Console.ReadLine();
        }
        static void PrintOutput(KqlOutput output)
        {
            Console.WriteLine("{0} {1} {2}",
                output.Comment.Trim('\n', '\r', '\t', ' '),
                output.Output["Symbol"],
                output.Output["Price"]);
        }

        static IEnumerable<IDictionary<string, object>> StockQuotes()
        {
            while(true)
            {
                int index = _random.Next(_symbols.Length);
                int price = _random.Next(100);

                var ticker = new Dictionary<string, object>();
                ticker.Add("Symbol", _symbols[index]);
                ticker.Add("Price", price);
                
                Thread.Sleep(10);
                yield return ticker;
            }
        }
    }
}
