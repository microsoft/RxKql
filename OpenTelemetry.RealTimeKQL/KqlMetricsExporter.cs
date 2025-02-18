using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTelemetry.RealTimeKQL
{
    class KqlMetricsExporter : KqlExporter<Metric>
    {
        public KqlMetricsExporter(string query)
            : base ("metrics", query) { }

        public override ExportResult Export(in Batch<Metric> batch)
        {
            foreach (var metric in batch)
            {
                foreach(var point in metric.GetMetricPoints())
                {
                    var dic = new Dictionary<string, object>();
                    dic.Add("Name", metric.Name);
                    dic.Add("Value", point.GetSumLong());
                    var tags = new Dictionary<string, object>();
                    dic.Add("Tags", tags);
                    foreach(var tag in point.Tags)
                    {
                        tags.Add(tag.Key, tag.Value);
                    }
                    _input.OnNext(dic);
                }
            }

            return ExportResult.Success;
        }
    }
}
