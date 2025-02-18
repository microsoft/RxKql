using OpenTelemetry;
using System.Diagnostics;

namespace OpenTelemetry.RealTimeKQL
{
    internal class KqlTraceExporter : KqlExporter<Activity>
    {
        public KqlTraceExporter(string query) 
            : base("traces", query) { }
 
        public override ExportResult Export(in Batch<Activity> batch)
        {
            foreach (Activity activity in batch)
            {
                var dic = new Dictionary<string, object>();
                dic.Add("Displayname", activity.DisplayName);
                dic.Add("Duration", activity.Duration);
                dic.Add("Id", activity.Id);
                dic.Add("ParentId", activity.ParentId);
                //var pairs = record.State as IEnumerable<KeyValuePair<string, object>>;
                //var data = new Dictionary<string, object>(pairs);
                //dic.Add("Data", data);     
                _input.OnNext(dic);
            }

            return ExportResult.Success;
        }
    }
}
