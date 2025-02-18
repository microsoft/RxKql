using OpenTelemetry;
using System.Reactive.Kql;
using System.Reactive.Kql.CustomTypes;
using System.Reactive.Subjects;

namespace OpenTelemetry.RealTimeKQL
{
    internal abstract class KqlExporter<T> : BaseExporter<T> where T : class
    {
        protected Subject<Dictionary<string, object>> _input = new Subject<Dictionary<string, object>>();
        protected string _streamName;
        KqlNodeHub _engine;

        public KqlExporter(string streamName, string query)
        {
            _streamName= streamName;
            _engine = KqlNodeHub.FromKqlQuery(_input, OnOutput, streamName, query);
        }

        void OnOutput(KqlOutput output)
        {
            WriteLine("--- {0} ---", _streamName);
            foreach (var pair in output.Output)
            {
                if (pair.Key == "{OriginalFormat}")
                    continue;

                var subItem = pair.Value as IDictionary<string, object>;
                if (subItem == null)
                {
                    WriteLine("{0} = {1}", pair.Key, pair.Value);
                }
                else
                {
                    PrintDictionary(pair.Key, subItem);
                }
            }
        }

        void PrintDictionary(string name, IDictionary<string, object> dic)
        {

            foreach (var pair in dic)
            {
                WriteLine("{0}.{1} = {2}", name, pair.Key, pair.Value);
            }
        }

        void WriteLine(string format, params object[] args)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = DemoColor(); 
            Console.WriteLine(format, args);
            Console.ForegroundColor = oldColor;
        }

        ConsoleColor DemoColor()
        {
            switch (_streamName)
            {
                case "audit":
                    return ConsoleColor.Red;
                case "logs":
                    return ConsoleColor.Yellow;
                case "traces": 
                    return ConsoleColor.DarkGray;
                case "metrics":
                    return ConsoleColor.Blue;

                default: throw new NotImplementedException();
            }
        }
    }
}
