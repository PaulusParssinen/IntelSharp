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

        /// <summary>
        /// The initial search request to obtain the search identifier.
        /// </summary>
        /// <param name="term">The search term.</param>
        /// <param name="buckets">The buckets to be queried for results.</param>
        /// <param name="timeout">The timeout in seconds.</param>
        /// <param name="maxResults">The maximum results to be queried per bucket, therefore the aggregated result set might be even bigger.</param>
        /// <param name="from">Result after the specified date. Both <paramref name="from"/> and <paramref name="to"/> are required if set.</param>
        /// <param name="to">Result before the specified date. Both <paramref name="from"/> and <paramref name="to"/> are required if set.</param>
        /// <param name="sorting">The sort order.</param>
        /// <param name="mediaType">The <see cref="MediaType"/> filter.</param>
        /// <param name="terminate">An array of search identifiers to be terminated. Can also be done with <seealso cref="TerminateAsync(Guid)"/></param>
        /// <exception cref="ArgumentException">Thrown when invalid search <paramref name="term"/> is submitted.</exception>
        /// <returns>The search identifier to be used to retrieve the results.</returns>
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

            if (response.Status == SearchStatus.InvalidTerm)
                throw new ArgumentException("Invalid input term.", nameof(term));

            return response.Id;
        }
        public async Task<(SearchResultStatus, IEnumerable<Item>)> FetchResultsAsync(Guid searchId, int offset = 0, int limit = 100)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", searchId },
                { "offset", offset },
                { "limit", limit }
            };

            var response = await IXAPI.GetAsync<SearchResultResponse>(_context,
                "/intelligent/search/result", parameters).ConfigureAwait(false);

            //TODO: validation & stuff prob

            return (response.Status, response.Records);
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
