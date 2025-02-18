using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using OpenTelemetry.RealTimeKQL;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reactive.Kql;

namespace ServiceTelemetry
{
    /// <summary>
    /// Fake "Collaboration Wiki" implementation, in which the methods look like invoking a remote Wiki
    /// In reality the implementation is almost non-existant (no website, no HTTP requests, etc.) as
    /// the purpose of this samle is to show the instrumentation for OpenTelemetry
    /// </summary>
    class CollaborationWiki : IDisposable
    {
        const string sampleName = "ModularSIEM.Samples.OTelComplete";
        const string configFileName = "config.kql";

        ILoggerFactory _auditFactory;
        ILogger _audit;
        ILoggerFactory _loggerFactory;
        ILogger _logger;
        ActivitySource _spans;
        TracerProvider _tracerProvider;
        Meter _meter = new(sampleName, "1.0");
        MeterProvider _meterProvider;
        Counter<long> _docReadCounter;
        Thread _configWatchThread;

        public CollaborationWiki()
        {
            BuildTelemetryPipelines();
            _configWatchThread = new Thread(WatchConfigChanges);
            _configWatchThread.Start();
        }

        public string ReadDocument(string user, string document, string ipAddress)
        {
            // Produce data on the "traces" real-time stream
            using (_spans.StartActivity(document))

            // Produce data on the "logs" real-time stream
            _logger.LogInformation("User {user} accessed {document} from {ipAddress}", user, document, ipAddress);

            return ReadFromStorage(document);
        }
        public void GrantPermission(string grantor, string grantee, string document, string ipAddress)
        {
            // Produce data on the "audit" real-time stream
            _audit.LogInformation("{grantor} granted write permission to {grantee} for {document} from {ipAddress}", grantor, grantee, document, ipAddress);
        }
        private string ReadFromStorage(string document)
        {
            using (_spans.StartActivity("ReadFromStorage"))

            // Also, produce counters on the "metrics" real-time stream
            _docReadCounter.Add(1, new KeyValuePair<string, object?>("document", document));
            Thread.Sleep(100); // delay, simulating real operation

            return document;
        }


        #region OpenTelemetry pipieline creataion
        string auditQuery;
        string logsQuery;
        string tracesQuery;
        string metricsQuery;

        /// <summary>
        /// Building four different real-time pipelines, each controlled with different KQL query
        /// The result shows interleaved with different colors on the console
        /// TODO: Separate this as a common library component
        /// </summary>
        void BuildTelemetryPipelines()
        {
            if (_meterProvider!= null) _meterProvider.Dispose();
            if (_loggerFactory!= null) _loggerFactory.Dispose();
            if (_tracerProvider!= null) _tracerProvider.Dispose();


            // By default, don't propagate any data
            auditQuery = "audit | where false";
            logsQuery = "logs | where false";
            tracesQuery = "traces | where false";
            metricsQuery = "metrics | where false";

            // Read the config file to see what is the data of interest
            // There is a saparate KQL query for each stream
            var queries = CslParser.ReadFile(configFileName);
            foreach (var query in queries)
            {
                Query q = new Query(query.Query);
                string streamName = q.Table.ToLowerInvariant();
                switch (streamName)
                {
                    case "audit":
                        auditQuery = query.Query; break;
                    case "logs":
                        logsQuery = query.Query; break;
                    case "traces":
                        tracesQuery = query.Query; break;
                    case "metrics":
                        metricsQuery = query.Query; break;
                }
            }

            // Setup OpenTelemetry pipeline for logging
            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddOpenTelemetry(options =>
                {
                    //options.AddConsoleExporter();
                    options.AddKqlExporter("logs", logsQuery);
                });
            });
            _logger = _loggerFactory.CreateLogger<Program>();

            // Setup OpenTelemetry pipeline for audit
            _auditFactory = LoggerFactory.Create(builder =>
            {
                builder.AddOpenTelemetry(options =>
                {
                    //options.AddConsoleExporter();
                    options.AddKqlExporter("audit", auditQuery);
                });
            });
            _audit = _auditFactory.CreateLogger<CollaborationWiki>();

            // Setup pipeline for traces, based on teh sample bellow:
            // 
            _spans = new ActivitySource(sampleName);
            _tracerProvider = Sdk.CreateTracerProviderBuilder()
                .AddSource(sampleName)
                //.AddConsoleExporter()
                .AddKqlExporter(tracesQuery)
                .Build();

            // Setup pipeline for metrics, based on the sample below:
            // https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/docs/metrics/getting-started/Program.cs
            _docReadCounter = _meter.CreateCounter<long>("ReadCounter");

            var readerOptions = new PeriodicExportingMetricReaderOptions()
            {
                ExportIntervalMilliseconds = 1000
            };

            _meterProvider = Sdk.CreateMeterProviderBuilder()
                .AddMeter(sampleName)
                .AddKqlExporter(metricsQuery)
                //.AddConsoleExporter((co, mo) => { mo.PeriodicExportingMetricReaderOptions = readerOptions; })
                .Build();
        }

        /// <summary>
        /// Refreshes the configuration when the file config.kql is udated
        /// This allows to change what is the data of interest at runtime
        /// </summary>
        public void WatchConfigChanges() 
        {
            var lastWrite = File.GetLastWriteTime(configFileName);
            while (true)
            {
                Thread.Sleep(100);
                try
                {
                    if (lastWrite == File.GetLastWriteTime(configFileName))
                        continue;

                    Console.WriteLine("*** {0}: lastWrite {1}", Thread.CurrentThread.ManagedThreadId, lastWrite);
                    BuildTelemetryPipelines();
                    lastWrite = File.GetLastWriteTime(configFileName);
                }
                catch (IOException ex)
                {
                    Console.WriteLine("*** {0}: {1}", DateTime.Now, ex.Message);
                }
            }
        }

        public void Dispose()
        {
            _meterProvider.Dispose();
            _meter.Dispose();
        }

        #endregion
    }
}

