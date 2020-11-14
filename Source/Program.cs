using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reactive.Kql;
using System.Threading;

namespace RxKqlNodeSample
{
    class Program
    {
        static Random _random = new Random();
        static string[] _symbols = { "MSFT", "AAPL", "AMZN" };

        static void Main()
        {
            KqlNode node = new KqlNode();
            node.AddCslFile(@"..\..\MsftQueries.csl");
            node.AddCslFile(@"..\..\AnySymbolQueries.csl");


            using (node.Subscribe(alert => Console.WriteLine("{0} {1} {2}", 
                alert.Comment.Trim('\n','\r','\t',' '),
                alert.AlertOccurrance["Symbol"],
                alert.AlertOccurrance["Price"])))
            {
                while (true)
                {
                    var ticker = GenerateTicker();
                    node.OnNext(ticker);
                    Thread.Sleep(10);
                }
            }
        }

        static IDictionary<string, object> GenerateTicker()
        {
            int index = _random.Next(_symbols.Length);
            int price = _random.Next(100);

            dynamic ticker = new ExpandoObject();
            ticker.Symbol = _symbols[index];
            ticker.Price = price;

            return ticker;
        }
    }
}
