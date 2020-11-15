using System;
using System.CommandLine;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.CommandLine.Invocation;

using IntelSharp.Model;

namespace IntelSharp.Sandbox
{
    class Program
    {
        //TODO: Add command for just phonebook search. Also maybe demonstrate the export etc.
        static Task<int> Main(string[] args)
        {
            Console.Title = "IntelSharp.Sandbox";

            var timeoutOption = new Option<int>("--timeout", 
                getDefaultValue: () => 0,
                description: "The search timeout in seconds.");

            var searchCommand = new Command("search")
            {
                new Argument<string>("term"),
                timeoutOption
            };
            searchCommand.Handler = CommandHandler.Create<string, int, string>(HandleIntelligentSearchAsync);

            var rootCommand = new RootCommand
            {
                searchCommand,

                new Option<string>("key")
                { 
                    Description = "Your Intelligence X API key.",
                    IsRequired = true
                }
            };

            rootCommand.Description = "An example CLI application using the Intel# .NET library";

            return rootCommand.InvokeAsync(args);
        }

        private static async Task<int> HandleIntelligentSearchAsync(string term, int timeout, string key)
        {
            var apiContext = new IXApiContext(key);

            var searchApi = new IntelligentSearchApi(apiContext);

            //Get the search result identifier in order to fetch the results.
            Guid resultId = await searchApi.SearchAsync(term, timeout: timeout);

            //Fetch the results using the obtained search result identifier
            (SearchResultStatus searchStatus, IEnumerable<Item> items) = await searchApi.FetchResultsAsync(resultId);

            foreach (Item item in items)
            {
                //Lil hack for stuff to fit in to terminal
                int cutIndex = item.Name.LastIndexOf('/') + 1;
                Console.WriteLine($"[..{item.Name[cutIndex..]}] Type: {item.Type}, Bucket: {item.Bucket}, Size: {item.Size} bytes");
            }

            await searchApi.TerminateAsync(resultId);

            return 1;
        }
    }
}
