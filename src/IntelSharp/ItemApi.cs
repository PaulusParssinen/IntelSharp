using System;
using System.Threading;
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

        public Task<XScoreExplanation> ExplainXScoreAsync(Item item, CancellationToken cancellationToken = default)
        {
            return ExplainXScoreAsync(item.SystemId, item.Bucket, cancellationToken);
        }
        public Task<XScoreExplanation> ExplainXScoreAsync(Guid systemId, string bucket, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", systemId },
                { "bucket", bucket }
            };

            return IXAPI.GetAsync<XScoreExplanation>(_context, 
                "/item/explain/xscore", parameters, cancellationToken: cancellationToken);
        }

        public Task<IEnumerable<Selector>> ListSelectorsAsync(Item item, CancellationToken cancellationToken = default)
        {
            return ListSelectorsAsync(item.SystemId, item.Bucket, cancellationToken);
        }
        public async Task<IEnumerable<Selector>> ListSelectorsAsync(Guid systemId, string bucket, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", systemId },
                { "bucket", bucket }
            };

            var response = await IXAPI.GetAsync<ListSelectorsResponse>(_context, 
                "/item/selector/list/human", parameters, cancellationToken: cancellationToken).ConfigureAwait(false);

            return response.Selectors;
        }
    }
}
