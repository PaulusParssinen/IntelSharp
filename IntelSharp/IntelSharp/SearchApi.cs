using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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

            //TODO: Status

            return response.Id;
        }
        public async Task<IEnumerable<Item>> FetchResultsAsync(Guid searchId, int offset = 0, int limit = 100)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", searchId },
                { "offset", offset },
                { "limit", limit }
            };

            var response = await IXAPI.GetAsync<SearchResultResponse>(_context,
                "/intelligent/search/result", parameters).ConfigureAwait(false);

            //TODO: SearchResultStatus to caller & validation

            return response.Records;
        }
        public async Task<SearchStatistic> GetStatisticsAsync(Guid searchId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", searchId }
            };

            return await IXAPI.GetAsync<SearchStatistic>(_context,
                "/intelligent/search/statistic", parameters).ConfigureAwait(false);
        }

        public async Task<AuthenticationInfo> GetAuthenticationInfoAsync()
        {
            return await IXAPI.GetAsync<AuthenticationInfo>(_context,
                "/authenticate/info").ConfigureAwait(false);
        }

        public async Task TerminateAsync(Guid searchId) { throw new NotImplementedException(); }
        public async Task ExportAsync(Guid searchId) { throw new NotImplementedException(); }
    }
}
