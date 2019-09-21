using System;
using System.Threading.Tasks;

using IntelSharp.Model;

namespace IntelSharp
{
    public class SearchApi
    {
        private readonly IXApiContext _context;

        public SearchApi(IXApiContext context)
        {
            _context = context;
        }

        public async Task<Guid> SearchAsync(string term,
            string[] buckets = default,
            int timeout = 0, int maxResults = 0,
            DateTime from = default, DateTime to = default,
            SortType sorting = default,
            MediaType mediaType = default,
            Guid[] terminate = default)
        {
            var searchRequest = new SearchRequest
            {
                Term = term,
                Buckets = buckets ?? new string[0],
                Timeout = timeout,
                MaxResults = maxResults,
                DateFrom = from,
                DateTo = to,
                Sort = sorting,
                Media = mediaType,
                Terminate = terminate ?? new Guid[0]
            };

            var response = await IXAPI.PostAsync<SearchResponse>(_context,
                "/intelligent/search", searchRequest).ConfigureAwait(false);

            //TODO: Statuses

            return response.Id;
        }

        public async Task FetchResultsAsync() { throw new NotImplementedException(); }
        public async Task TerminateAsync() { throw new NotImplementedException(); }
    }
}
