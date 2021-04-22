using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reactive.Kql;
using System.Reflection;
using System.Threading;

namespace RxKqlNodeSample
{
    class Program
    {
        static Random _random = new Random();
        static string[] _symbols = { "MSFT", "AAPL", "AMZN" };

        static void Main()
        {
            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DirectoryInfo dir_info = new DirectoryInfo(directoryName);

            KqlNode node = new KqlNode();

            node.AddCslFile(Path.Combine(dir_info.FullName, @"MsftQueries.csl"));
            node.AddCslFile(Path.Combine(dir_info.FullName, @"AnySymbolQueries.csl"));

            using (node.Subscribe(alert => Console.WriteLine("{0} {1} {2}", 
                alert.Comment.Trim('\n','\r','\t',' '),
                alert.Output["Symbol"],
                alert.Output["Price"])))
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
