# Intel#
.NET Core 3.1 library for [Intelligence X](https://intelx.io/) API

## Usage
### Requirements
+ .NET Core 3.1 SDK

### Setup
1. Install the [IntelSharp NuGet Package](https://www.nuget.org/packages/IntelSharp/)

.NET CLI: 
```
dotnet add package IntelSharp --version 1.0.0
```
Package Manager: 
```
Install-Package IntelSharp -Version 1.0.0
```

2. Grab your personal Intelligence X API key from "Account > Developers" -tab.

### Snippet
```C#
using IntelSharp;
using IntelSharp.Model;
..

//Initialization
var apiContext = new IXApiContext("your-api-key");
var searchApi = new IntelligentSearchApi(apiContext);

//Create a new active search job
Guid searchIdentifier = await searchApi.SearchAsync(term: "nsa.gov", ...);

//Obtain search status and its items.
var (searchStatus, items) = await searchApi.FetchResultsAsync(searchIdentifier);

//Work with the result items
foreach (Item item in items)
{ ... }

//For example, you can use FileApi to download and view the contents of the found items.
var fileApi = new FileApi(apiContext);
byte[] data = await fileApi.ReadAsync(item: ...);


//Remember to terminate the search job!
await searchApi.TerminateAsync(searchIdentifier);
```

## IntelSharp.Sandbox
IntelSharp.Sandbox is small CLI application demonstrating basic features of Intel# library.
### Prerequisites
+ [.NET Core Runtime](https://dotnet.microsoft.com/download/dotnet-core/current/runtime ".NET Core Runtime")

### Usage
```
$ dotnet IntelSharp.Sandbox.dll -?
IntelSharp.Sandbox:
  An example CLI application using the Intel# .NET Core library

Usage:
  IntelSharp.Sandbox [options] [command]

Options:
  --key <key>            Your Intelligence X API key.
  --timeout <timeout>    The search timeout in seconds.
  --version              Show version information
  -?, -h, --help         Show help and usage information

Commands:
  search <term>
  ```
