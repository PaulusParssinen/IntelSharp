using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using IntelSharp.Model;

namespace IntelSharp.Sandbox
{
    public class Program
    {
        private readonly IXApiContext _context;

        public Program(Queue<string> args)
        {
            _context = new IXApiContext(baseUrl: args.Dequeue(), key: args.Dequeue());
        }

        public async Task RunAsync(Queue<string> args)
        {
            string searchTerm = args.Dequeue();

            var searchApi = new SearchApi(_context);
            var authInfo = await searchApi.GetAuthenticationInfoAsync();

            Guid resultId = await searchApi.SearchAsync(searchTerm);

            //Fetch results using the obtained search result job identifier
            var (resultStatus, items) = await searchApi.FetchResultsAsync(resultId);

            Console.WriteLine($"[{resultId}] Records for the term: " + searchTerm);
            foreach (Item item in items)
            {
                Console.WriteLine($" [{item.SystemId}] Type: {item.Type}, Bucket: {item.Bucket}, Size: {item.Size} bytes");
            }

            var stats = await searchApi.GetStatisticsAsync(resultId);
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("IntelSharp - Sandbox");
            try
            {
                var queue = new Queue<string>(args);

                var program = new Program(queue);
                program.RunAsync(queue).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown: " + ex);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
