using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using IntelSharp.Model;
using IntelSharp.Model.Search;

namespace IntelSharp
{
    /// <summary>
    /// Allows performing search queries using various types of search terms.
    /// </summary>
    /// <typeparam name="TResult">The result item type.</typeparam>
    public abstract class SearchApi<TResult>
    {
        protected readonly string _apiPathSegment;
        protected readonly IXApiContext _context;

        protected SearchApi(string apiPathSegment, IXApiContext context)
        {
            _apiPathSegment = apiPathSegment;
            _context = context;
        }

        /// <summary>
        /// The initial search request to obtain the search identifier.
        /// </summary>
        /// <param name="term">The search term. Must be valid <see cref="SelectorType"/>.</param>
        /// <param name="buckets">The buckets to be queried for results.</param>
        /// <param name="timeout">The timeout in seconds.</param>
        /// <param name="maxResults">The maximum results to be queried per bucket, therefore the aggregated result set might be even bigger.</param>
        /// <param name="from">Result after the specified date. Both <paramref name="from"/> and <paramref name="to"/> are required if set.</param>
        /// <param name="to">Result before the specified date. Both <paramref name="from"/> and <paramref name="to"/> are required if set.</param>
        /// <param name="sorting">The sort order.</param>
        /// <param name="mediaType">The <see cref="MediaType"/> filter.</param>
        /// <param name="terminate">An array of search identifiers to be terminated. Can also be done with <seealso cref="TerminateAsync(Guid, CancellationToken)"/></param>
        /// <exception cref="ArgumentException">Thrown when invalid search <paramref name="term"/> is submitted.</exception>
        /// <returns>The search identifier to be used to retrieve the results.</returns>
        public async Task<Guid> SearchAsync(string term,
            string[] buckets = default,
            int timeout = 0, int maxResults = 0,
            DateTime? from = default, DateTime? to = default,
            SortType sorting = default,
            MediaType mediaType = default,
            Guid[] terminate = default, 
            CancellationToken cancellationToken = default)
        {
            var searchRequest = new SearchRequest
            {
                Term = term,
                Buckets = buckets ?? Array.Empty<string>(),
                Timeout = timeout,
                MaxResults = maxResults,
                DateFrom = from,
                DateTo = to,
                Sort = sorting,
                Media = mediaType,
                Terminate = terminate ?? Array.Empty<Guid>()
            };
            
            var response = await IXAPI.PostAsync<SearchResponse>(_context,
                _apiPathSegment + "/search", searchRequest, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (response.Status == SearchStatus.InvalidTerm)
                throw new ArgumentException("Invalid input term.", nameof(term));

            return response.Id;
        }

        /// <summary>
        /// Returns the actual search results for the specified search identifier.
        /// </summary>
        /// <param name="searchId">The identifier of search job.</param>
        /// <param name="offset">The search offset to start from.</param>
        /// <param name="limit">Maximum amount of results.</param>
        public abstract Task<(SearchResultStatus, IEnumerable<TResult>)> FetchResultsAsync(Guid searchId, int offset = 0, int limit = 100, CancellationToken cancellationToken = default);

        /// <summary>
        /// Terminates the search job, no-op if the search was already terminated.
        /// </summary>
        /// <param name="searchId">The identifier of search to be terminated.</param>
        public async Task TerminateAsync(Guid searchId, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", searchId }
            };

            await IXAPI.PostAsync<string>(_context,
                "/intelligent/search/terminate", parameters, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        //public async Task ExportAsync(Guid searchId, int format, CancellationToken cancellationToken = default) => throw new NotImplementedException(); //TODO:
    }
}
