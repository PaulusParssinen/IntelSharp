using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using IntelSharp.Model;

namespace IntelSharp
{
    public class FileApi
    {
        private readonly IXApiContext _context;

        public FileApi(IXApiContext context)
        {
            _context = context;
        }

        public async Task<byte[]> PreviewAsync(Item item)
        {
            return await PreviewAsync(item.StorageId, DataType.Plaintext, MediaType.Text, item.Bucket).ConfigureAwait(false);
        }
        public async Task<byte[]> PreviewAsync(string storageId, DataType contentType, MediaType mediaType, string bucket)
        {
            var parameters = new Dictionary<string, object>
            {
                { "sid", storageId },
                { "l", 20 },
                { "f", 0 },
                { "c", contentType }, //idk tbh, gon research more
                { "m", mediaType },
                { "b", bucket },
            };

            return await IXAPI.GetAsync<byte[]>(_context, "/file/preview", parameters).ConfigureAwait(false);
        }

        public async Task<byte[]> ReadAsync(Item item)
        {
            return await ReadAsync(item.StorageId, item.Bucket).ConfigureAwait(false);
        }
        public async Task<byte[]> ReadAsync(string storageId, string bucket)
        {
            var parameters = new Dictionary<string, object>
            {
                { "type", 1 }, //TODO: ehhh
                { "storageid", storageId },
                { "bucket", bucket }
            };

            return await IXAPI.GetAsync<byte[]>(_context, "/file/read", parameters).ConfigureAwait(false);
        }

        public async Task<byte[]> ViewAsync(Item item, string license) //TODO: why the license tho
        {
            return await ViewAsync(item.StorageId, item.Bucket, license).ConfigureAwait(false);
        }
        public async Task<byte[]> ViewAsync(string storageId, string bucket, string license)
        {
            var parameters = new Dictionary<string, object>
            {
                { "f", 0 }, //TODO:
                { "storageid", storageId },
                { "bucket", bucket },
                { "license", license }
            };

            return await IXAPI.GetAsync<byte[]>(_context, "/file/view", parameters).ConfigureAwait(false);
        }
    }
}
