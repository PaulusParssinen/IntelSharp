using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace IntelSharp.Sandbox
{
    public class Program
    {
        private readonly IXApiContext _context;

        public Program(Queue<string> args)
        {
            _context = new IXApiContext(args.Dequeue(), args.Dequeue());
        }

        public async Task RunAsync(Queue<string> args)
        {
            var searchApi = new SearchApi(_context);

            Guid resultId = await searchApi.SearchAsync(args.Dequeue());
            
            //Fetch results using the obtained search result identifier 
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
