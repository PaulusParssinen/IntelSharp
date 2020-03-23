using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using IntelSharp.Model;
using IntelSharp.Model.Search;

namespace IntelSharp
{
    /// <inheritdoc cref="SearchApi{TResult, TResultResponse}"/>
    public class IntelligentSearchApi : SearchApi<Item, IntelligentSearchResults>
    {
        public IntelligentSearchApi(IXApiContext context)
            : base("/intelligent", context)
        { }

        public override async Task<(SearchResultStatus, IEnumerable<Item>)> FetchResultsAsync(Guid searchId, int offset = 0, int limit = 100)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", searchId },
                { "offset", offset },
                { "limit", limit }
            };

            var response = await IXAPI.GetAsync<IntelligentSearchResults>(_context,
                _apiPathSegment + "/search/result", parameters).ConfigureAwait(false);

            return (response.Status, response.Records);
        }

        /// <summary>
        /// Fetches statistics about the search result items.
        /// </summary>
        public async Task<SearchStatistic> GetStatisticsAsync(Guid searchId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", searchId }
            };

            return await IXAPI.GetAsync<SearchStatistic>(_context,
                "/intelligent/search/statistic", parameters).ConfigureAwait(false);
        }
    }
}
