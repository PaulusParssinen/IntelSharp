using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using IntelSharp.Model;

namespace IntelSharp
{
    /// TODO: Streams
    /// <summary>
    /// This API contains the methods to retrieve contents of an <see cref="Item"/>.
    /// </summary>
    #nullable enable
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
        public Task<byte[]> ReadAsync(Item item, CancellationToken cancellationToken = default)
        {
            return ReadAsync(item.StorageId, item.Bucket, cancellationToken);
        }
        /// <inheritdoc cref="ReadAsync(Item, CancellationToken)"/>
        /// <param name="storageId">The <see cref="Item.StorageId">storage identifier</see> of the item.</param>
        /// <param name="bucket">The bucket in which the item is located.</param>
        public Task<byte[]> ReadAsync(string storageId, string? bucket = default, CancellationToken cancellationToken = default)
        {
            const int OUTPUT_TYPE_RAW_BINARY = 0;

            var parameters = new Dictionary<string, object>
            {
                { "type", OUTPUT_TYPE_RAW_BINARY },
                { "storageid", storageId },
                { "bucket", bucket ?? string.Empty }
            };

            return IXAPI.GetAsync<byte[]>(_context, "/file/read", parameters, cancellationToken: cancellationToken);
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
        public Task<byte[]> ViewAsync(Item item, ViewFormat format = ViewFormat.Text, bool escapeHtml = true, CancellationToken cancellationToken = default)
        {
            return ViewAsync(item.StorageId, item.Bucket, format, escapeHtml, cancellationToken);
        }
        /// <inheritdoc cref="ViewAsync(Item, ViewFormat, bool, CancellationToken)"/>
        /// <param name="storageId">The <see cref="Item.StorageId">storage identifier</see> of the item.</param>
        /// <param name="bucket">The bucket in which the item is located.</param>
        public Task<byte[]> ViewAsync(
            string storageId, 
            string bucket, 
            ViewFormat format = ViewFormat.Text,
            bool escapeHtml = true,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "f", format },
                { "storageid", storageId },
                { "bucket", bucket },
                { "escape", escapeHtml ? 1 : 0 }
            };

            return IXAPI.GetAsync<byte[]>(_context, "/file/view", parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Provides a preview of item's contents.
        /// </summary>
        /// <param name="item">The item to be previewed.</param>
        /// <param name="lineCount">The amount of lines to be shown in preview.</param>
        /// <param name="escapeHtml">Should the output HTML be escaped.</param>
        /// <exception cref="InvalidOperationException"/>
        /// <returns>The preview of data.</returns>
        public Task<byte[]> PreviewAsync(Item item, int lineCount = 12, bool escapeHtml = true, CancellationToken cancellationToken = default)
        {
            return PreviewAsync(item.StorageId, item.Type, item.MediaType, item.Bucket, lineCount, escapeHtml, cancellationToken);
        }
        /// <inheritdoc cref="PreviewAsync(Item, int, bool, CancellationToken)"/>
        /// <param name="storageId">The <see cref="Item.StorageId">storage identifier</see> of the item.</param>
        /// <param name="dataType">The low-level type representaton of the item's data type. Must be same as the source item's.</param>
        /// <param name="mediaType">The high-level type representaton of the item's data type. Must be same as the source item's.</param>
        /// <param name="bucket">The bucket in which the item is located.</param>
        public Task<byte[]> PreviewAsync(
            string storageId,
            DataType dataType,
            MediaType mediaType,
            string bucket,
            int lineCount = 12, 
            bool escapeHtml = true,
            CancellationToken cancellationToken = default)
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

            return IXAPI.GetAsync<byte[]>(_context, "/file/preview", parameters, cancellationToken: cancellationToken);
        }
    }
}
