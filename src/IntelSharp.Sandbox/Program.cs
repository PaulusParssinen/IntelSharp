using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.CommandLine;
using System.CommandLine.Invocation;

using IntelSharp.Model;

namespace IntelSharp.Sandbox
{
    class Program
    {
        //TODO: Add command for just phonebook search. Also maybe demonstrate the export etc.
        static async Task<int> Main(string[] args)
        {
            Console.Title = "IntelSharp.Sandbox";

            var searchCommand = new Command("search")
            {
                new Argument<string>("term")
                {
                    Description = "A search term."
                }
            };
            searchCommand.Handler = CommandHandler.Create<InvocationContext, string, int, string>(HandleIntelligentSearch);
            
            var rootCommand = new RootCommand
            {
                searchCommand,

                new Option<string>("--key",
                    description: "Your Intelligence X API key."),
                new Option<int>("--timeout",
                    getDefaultValue: () => 0,
                    description: "The search timeout")
            };

            rootCommand.Description = "An example CLI application using the Intel# .NET Core library";

            return await rootCommand.InvokeAsync(args);
        }

        private static async Task<int> HandleIntelligentSearch(InvocationContext context, string term, int timeout, string key)
        {
            var apiContext = new IXApiContext
            {
                Key = key
            };

            var searchApi = new IntelligentSearchApi(apiContext);

            //Get the search result identifier in order to fetch the results.
            Guid resultId = await searchApi.SearchAsync(term);

            //Fetch the results using the obtained search result identifier
            (SearchResultStatus resultStatus, IEnumerable<Item> items) = await searchApi.FetchResultsAsync(resultId);

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
