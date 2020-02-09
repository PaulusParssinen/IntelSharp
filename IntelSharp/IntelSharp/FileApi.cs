using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using IntelSharp.Model;

namespace IntelSharp
{
    /// TODO: Streams
    /// <summary>
    /// This API contains the methods to retrieve contents of an <see cref="Item"/>.
    /// </summary>
    public class FileApi
    {
        private readonly IXApiContext _context;

        public FileApi(IXApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves an <see cref="Item"/>'s raw data. 
        /// <para/>
        /// Maximum output sizes: <br/>
        /// Free license: 10 MB <br/>
        /// Professional license: 100 MB
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The raw binary data of the item.</returns>
        public async Task<byte[]> ReadAsync(Item item)
        {
            return await ReadAsync(item.StorageId, item.Bucket).ConfigureAwait(false);
        }
        /// <inheritdoc cref="ReadAsync(Item)"/>
        /// <param name="storageId">The <see cref="Item.StorageId">storage identifier</see> of the item.</param>
        /// <param name="bucket">The bucket in which the item is located.</param>
        public async Task<byte[]> ReadAsync(string storageId, string bucket = default)
        {
            const int OUTPUT_TYPE_RAW_BINARY = 0;

            var parameters = new Dictionary<string, object>
            {
                { "type", OUTPUT_TYPE_RAW_BINARY },
                { "storageid", storageId },
                { "bucket", bucket }
            };

            return await IXAPI.GetAsync<byte[]>(_context, "/file/read", parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Fetches the data in wanted output format. Good use-case when displaying content to end-user.
        /// <para/>
        /// Maximum output sizes: <br/>
        /// Free license: 4 MB <br/>
        /// Professional license: 6 MB
        /// </summary>
        /// <param name="item">The item to be formatted.</param>
        /// <param name="format">The output format data will be printed in.</param>
        /// <param name="escapeHtml">Should the output HTML be escaped.</param>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="Exception">The item was not found.</exception>
        public async Task<byte[]> ViewAsync(Item item, ViewFormat format = ViewFormat.Text, bool escapeHtml = true)
        {
            return await ViewAsync(item.StorageId, item.Bucket, format, escapeHtml).ConfigureAwait(false);
        }
        /// <inheritdoc cref="ViewAsync(Item, ViewFormat, bool)"/>
        /// <param name="storageId">The <see cref="Item.StorageId">storage identifier</see> of the item.</param>
        /// <param name="bucket">The bucket in which the item is located.</param>
        public async Task<byte[]> ViewAsync(
            string storageId, 
            string bucket, 
            ViewFormat format = ViewFormat.Text,
            bool escapeHtml = true)
        {
            var parameters = new Dictionary<string, object>
            {
                { "f", format },
                { "storageid", storageId },
                { "bucket", bucket },
                { "escape", escapeHtml ? 1 : 0 }
            };

            return await IXAPI.GetAsync<byte[]>(_context, "/file/view", parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Provides a preview of item's contents.
        /// </summary>
        /// <param name="item">The item to be previewed.</param>
        /// <param name="lineCount">The amount of lines to be shown in preview.</param>
        /// <param name="escapeHtml">Should the output HTML be escaped.</param>
        /// <exception cref="InvalidOperationException"/>
        /// <returns>The preview of data.</returns>
        public async Task<byte[]> PreviewAsync(Item item, int lineCount = 12, bool escapeHtml = true)
        {
            return await PreviewAsync(item.StorageId, item.Type, item.MediaType, item.Bucket, lineCount, escapeHtml).ConfigureAwait(false);
        }
        /// <inheritdoc cref="PreviewAsync(Item, int, bool)"/>
        /// <param name="storageId">The <see cref="Item.StorageId">storage identifier</see> of the item.</param>
        /// <param name="dataType">The low-level type representaton of the item's data type. Must be same as the source item's.</param>
        /// <param name="mediaType">The high-level type representaton of the item's data type. Must be same as the source item's.</param>
        /// <param name="bucket">The bucket in which the item is located.</param>
        public async Task<byte[]> PreviewAsync(
            string storageId,
            DataType dataType,
            MediaType mediaType,
            string bucket,
            int lineCount = 12, 
            bool escapeHtml = true)
        {
            var parameters = new Dictionary<string, object>
            {
                { "sid", storageId },
                { "b", bucket },
                { "l", lineCount },
                { "f", mediaType == MediaType.Picture ? 1 : 0 },
                { "c", dataType },
                { "m", mediaType },
                { "e", escapeHtml ? 1 : 0 }
            };

            return await IXAPI.GetAsync<byte[]>(_context, "/file/preview", parameters).ConfigureAwait(false);
        }
    }
}
