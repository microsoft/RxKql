# Advanced Rx.KQL
Rx.KQL v3 allows users the abstraction of Reactive Extensions and it's complexity. 

### KqlNode class

KqlNode is a class hides the internal complexity of Rx, LINQ and Lambda-Expressions and brings the power of real-time query to users that only want to learn KQL.

Useful analogy is that KqlNode is a host, that keeps many simultaneously running real-time, standing queries:

    KqlNode node = new KqlNode();
    node.AddCslFile(@"..\..\MsftQueries.csl");
    node.AddCslFile(@"..\..\AnySymbolQueries.csl"); 

### KqlNodeHub class

KqlNodeHub is a class hides the internal complexity of Rx, LINQ and Lambda-Expressions and brings the power of real-time query to users that only want to learn KQL.

Useful analogy is that KqlNode is a host, that keeps many simultaneously running real-time, standing queries:

    var v3node = KqlNodeHub.FromFiles(_etw, DefaultOutput, "TCP", "TraficByIP.csl");
    -- or --
    var v3Node = KqlNodeHub.FromKqlQuery(_etw, DefaultOutput,  "TCP", {kqlQuery});
 
    static void DefaultOutput(KqlOutput e)
    {
        var output = e.Output; 
        Console.WriteLine("{0,7} {1,15} {2,5} {3}", output["packets"], output["daddr"], output["pid"], output["process"]);
    }

Addtional functionality:
In Rx.Kql v3+, we have added the functionality introducing user function creation which will be evaluated at query execution when each event is processed.  An example below of 'GetProcessName' allows developers the 
ability to create their own custom functions in the hosting process, and with the required attribute on the method and registration with the ScalarFunctionFactory, Rx.Kql will call the developers method on next of each event as it arrives
if the KQL Query using the function references the function.  Take a quick look at the function as registered below.

    Warning!!!  Registering inefficient function calls will impede performance of event analysis.  Use with caution.

Here is a full Console application example of registering an ETW provider, using KQLNodeHub, and listening to ETW TCP events.

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Reactive;
	using System.Reactive.Kql;
	using System.Reactive.Kql.CustomTypes;
	using System.Reflection;
	using System.Security.Principal;
	using System.Text;
	using System.Threading.Tasks;
	using Tx.Windows;

    namespace Rx.Kql.TcpProcessLookup
	{
		class Program
		{
			const string SessionName = "tcp";
			readonly static Guid ProviderId = new Guid("{7dd42a49-5329-4832-8dfd-43d979153a88}");

			static IObservable<IDictionary<string, object>> _etw;
			public static void Main(string[] args)
			{
				StartSession(SessionName, ProviderId);
				_etw = EtwTdhObservable.FromSession(SessionName);

				ScalarFunctionFactory.AddFunctions(typeof(Program));

				var v3node = KqlNodeHub.FromFiles(_etw, DefaultOutput, "TraficByIP.csl");

				Console.WriteLine("Listening... press ENTER to terminate");
				Console.WriteLine();
				Console.WriteLine("packets         address   pid process");
				Console.ReadLine();
			}

			static void DefaultOutput(DetectionAlert e) // The type should not be called Detection nor Alert and should not be in separate namespace
			{
				var output = e.AlertOccurrence; // this is no longer alert. "Output" is better name for the property
				Console.WriteLine("{0,7} {1,15} {2,5} {3}", output["packets"], output["daddr"], output["pid"], output["process"]);
			}

			[KqlScalarFunction("getProcessName")]
			public static string GetProcessName(uint pid)
			{
				try
				{
					var p = Process.GetProcessById((int)pid);
					return p.ProcessName;
				}
				catch(Exception ex)
				{
					return null;
				}
			}

			static void StartSession(string sessionName, Guid providerId)
			{
				var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
				if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
					throw new Exception("To use ETW real-time session you must be administrator");

				Process logman = Process.Start("logman.exe", "stop " + sessionName + " -ets");
				logman.WaitForExit();

				logman = Process.Start("logman.exe", "create trace " + sessionName + " -rt -nb 2 2 -bs 1024 -p {" + providerId + "} 0xffffffffffffffff -ets");
				logman.WaitForExit();
			}
		}
	}

TrafficByIp.csl file content:
      
    EtwTcp 
    | where EventId == 11
    | extend daddr = EventData.daddr, pid = EventData.PID
    | summarize packets = count() by pid, daddr, bin(TimeCreated, 10s)
    | extend process = getProcessName(pid)

