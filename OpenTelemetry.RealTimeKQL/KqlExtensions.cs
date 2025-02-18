using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace OpenTelemetry.RealTimeKQL
{
    public static class KqlExtensions
    {
        public static OpenTelemetryLoggerOptions AddKqlExporter(this OpenTelemetryLoggerOptions options, string streamName, string query)
        {
            return options.AddProcessor(new BatchLogRecordExportProcessor(new KqlLogExporter(streamName, query), 1024, 100));
        }

        public static TracerProviderBuilder AddKqlExporter(this TracerProviderBuilder builder, string query)
        {
            return builder.AddProcessor(new SimpleActivityExportProcessor(new KqlTraceExporter(query)));
        }

        public static MeterProviderBuilder AddKqlExporter(this MeterProviderBuilder builder, string query)
        {
            return builder.AddReader(new PeriodicExportingMetricReader(new KqlMetricsExporter(query),1000));
        }
    }
}
