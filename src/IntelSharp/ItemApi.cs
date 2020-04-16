using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using IntelSharp.Model;

namespace IntelSharp
{
    public class ItemApi
    {
        private readonly IXApiContext _context;

        public ItemApi(IXApiContext context)
        {
            _context = context;
        }

        public Task<XScoreExplanation> ExplainXScoreAsync(Item item)
        {
            return ExplainXScoreAsync(item.SystemId, item.Bucket);
        }
        public async Task<XScoreExplanation> ExplainXScoreAsync(Guid systemId, string bucket)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", systemId },
                { "bucket", bucket }
            };

            return await IXAPI.GetAsync<XScoreExplanation>(_context,
                "/item/explain/xscore", parameters).ConfigureAwait(false);
        }

        public Task<IEnumerable<Selector>> ListSelectorsAsync(Item item)
        {
            return ListSelectorsAsync(item.SystemId, item.Bucket);
        }
        public async Task<IEnumerable<Selector>> ListSelectorsAsync(Guid systemId, string bucket)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", systemId },
                { "bucket", bucket }
            };

            var response = await IXAPI.GetAsync<ListSelectorsResponse>(_context,
                "/item/selector/list/human", parameters).ConfigureAwait(false);

            return response.Selectors;
        }
    }
}
