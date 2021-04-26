# Rx.Kql
Today Microsoft has two technologies which achieve seemingly unrelated goas: 
- the Kusto Query Language (KQL) which allows the user to **store, and then query** petabytes of data. This language is used in Azure Data Explorer, Azure Monitor, Log Analytics, Azure Sentinel, etc.
- Reactive Extensions, which works on push mode streams, processing one event at the time **without storage**. This is useful to build **real-time** applications with microseconds or milliseconds response time.    

These technologies have very different developer entry bar - learning KQL is no more difficult than learning how to use a database, while using Rx.Net requires advanced C# knowledge about LINQ, lambda expressions, Disposables, etc.

Rx.KQL is a C# library which brings the benefits of true-real-time into the simpler world of KQL users. 

## Samples
To bridge the gap to KQL users, the Rx.KQL library needs to be hosted in some C# application or system that has the streams of events. These samples illustrate how to do this.  

### Mouse move
This sample illustrates the usage of Rx.KQL on stream of mouse-move events in a local WinForms application. Advanced C# user with understanding of Rx and LNNQ can mix KQL in their **real-time** push pipelines.

See [Readme](Samples/MouseMove/Readme.md) and demo movie

### Multi-query 
This sample illustrates hosting Rx.KQL in a console app for processing infinite, real-time stream of stock tickers.

The main point is that we can run practically **infinite number** of real-time querues simultaneously. Each query is a push pipeline that processes each event as it arrives, and produces the result with microseconds delay.

See [Readme](Samples/MultiQuery/Readme.md) and demo movie

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
