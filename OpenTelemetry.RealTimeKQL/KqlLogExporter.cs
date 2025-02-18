using OpenTelemetry;
using OpenTelemetry.Logs;

namespace OpenTelemetry.RealTimeKQL
{
    internal class KqlLogExporter: KqlExporter<LogRecord>
    {
        public KqlLogExporter(string streamName, string query) 
            : base(streamName, query) 
        {
        }

        public override ExportResult Export(in Batch<LogRecord> batch)
        {
            foreach(var record in batch)
            {
                var dic = new Dictionary<string, object>();
                dic.Add("Timestamp", record.Timestamp);
                var pairs = record.State as IEnumerable<KeyValuePair<string, object>>;
                var data = new Dictionary<string, object>(pairs);
                dic.Add("Data", data);     
                _input.OnNext(dic);
            }

            return ExportResult.Success;
        }
    }
}
