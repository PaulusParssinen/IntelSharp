using System;
using System.IO;
using System.Linq;
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
            _context = new IXApiContext()
            {
                Key = args.Dequeue()
            };
        }

        public async Task RunAsync(Queue<string> args)
        {
            string searchTerm = args.Dequeue();

            var fileApi = new FileApi(_context);
            var searchApi = new SearchApi(_context);
            var phonebookApi = new PhonebookApi(_context);

            //Get the search result identifiers in order to fetch the results.
            Guid resultId = await searchApi.SearchAsync(searchTerm);
            Guid phonebookResultId = await phonebookApi.SearchAsync(searchTerm);

            //Fetch the results using the obtained search result identifiers
            var (resultStatus, items) = await searchApi.FetchResultsAsync(resultId);
            var (phonebookResultStatus, selectors) = await phonebookApi.FetchResultsAsync(phonebookResultId);

            Console.WriteLine($"Records for the term: " + searchTerm);
            foreach (Item item in items)
            {
                //Lil' hack for stuff to fit in to terminal
                int cutIndex = item.Name.LastIndexOf('/') + 1;
                Console.WriteLine($"[..{item.Name[cutIndex..]}] Type: {item.Type}, Bucket: {item.Bucket}, Size: {item.Size} bytes");
            }

            //Demonstrate File API download
            Item plaintextItem = items.FirstOrDefault(i => i.Type == DataType.Plaintext);
            if (plaintextItem != null)
            {
                Console.WriteLine("Writing first plaintext result's data to file..");

                byte[] fileContent = await fileApi.ReadAsync(plaintextItem);
                File.WriteAllBytes(plaintextItem.StorageId + ".txt", fileContent);

                Console.WriteLine("Done");
            }
            else Console.WriteLine("No plaintext item found.");

            SearchStatistic stats = await searchApi.GetStatisticsAsync(resultId);

            //Let's be nice and terminate our searches because we already have everything we need. Save the planet etc. by freeing server resources.
            await searchApi.TerminateAsync(resultId);
            await phonebookApi.TerminateAsync(phonebookResultId);
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
