using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using IntelSharp.Model;

namespace IntelSharp
{
    public class PhonebookApi
    {
        private readonly IXApiContext _context;

        public PhonebookApi(IXApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The initial search request to obtain the phonebook search identifier.
        /// </summary>
        /// <param name="term">The search term.</param>
        /// <param name="timeout">The timeout in seconds.</param>
        /// <param name="maxResults">The maximum amount of selector results to be queried.</param>
        /// <param name="mediaType">The <see cref="MediaType"/> filter.</param>
        /// <param name="terminate">An array of search identifiers to be terminated. Can also be done with <seealso cref="TerminateAsync(Guid)"/></param>
        /// <exception cref="ArgumentException">Thrown when invalid phonebook search <paramref name="term"/> is submitted.</exception>
        /// <returns>The phonebook search identifier to be used to retrieve the results.</returns>
        public async Task<Guid> SearchAsync(string term,
            int timeout = 0,
            int maxResults = 1000,
            MediaType mediaType = default,
            Guid[] terminate = default)
        {
            var searchRequest = new SearchRequest
            {
                Term = term,
                Timeout = timeout,
                MaxResults = maxResults,
                Media = mediaType,
                Terminate = terminate ?? new Guid[0]
            };

            var response = await IXAPI.PostAsync<SearchResponse>(_context,
                "/phonebook/search", searchRequest).ConfigureAwait(false);

            if (response.Status == SearchStatus.InvalidTerm)
                throw new ArgumentException("Invalid input term.", nameof(term));

            return response.Id;
        }

        public async Task<(SearchResultStatus, IEnumerable<PhonebookSelector>)> FetchResultsAsync(Guid searchId, int limit = 1000)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", searchId },
                { "limit", limit }
            };

            var response = await IXAPI.GetAsync<PhonebookResultResponse>(_context,
                "/phonebook/search/result", parameters).ConfigureAwait(false);

            //TODO: validation & stuff prob

            return (response.Status, response.Selectors);
        }

        /// <summary>
        /// Terminates the search, no-op if search was already terminated.
        /// </summary>
        /// <param name="searchId">The identifier of search to be terminated.</param>
        public async Task TerminateAsync(Guid searchId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", searchId }
            };

            await IXAPI.PostAsync<string>(_context,
                "/intelligent/search/terminate", parameters).ConfigureAwait(false);
        }
    }
}
