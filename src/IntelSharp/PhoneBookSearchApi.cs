using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using IntelSharp.Model;
using IntelSharp.Model.Search;

namespace IntelSharp
{
    /// <inheritdoc cref="SearchApi{TResult, TResultResponse}"/>
    public class PhoneBookSearchApi : SearchApi<PhoneBookSelector>
    {
        public PhoneBookSearchApi(IXApiContext context)
            : base("/phonebook", context)
        { }

        public override async Task<(SearchResultStatus, IEnumerable<PhoneBookSelector>)> FetchResultsAsync(Guid searchId, int offset = 0, int limit = 100)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", searchId },
                { "offset", offset },
                { "limit", limit }
            };

            var response = await IXAPI.GetAsync<PhoneBookSearchResults>(_context,
                _apiPathSegment + "/search/result", parameters).ConfigureAwait(false);

            return (response.Status, response.Selectors);
        }
    }
}
