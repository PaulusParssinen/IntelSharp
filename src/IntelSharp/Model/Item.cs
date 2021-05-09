using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IntelSharp.Model
{
    /// <summary>
    /// Represents the item's metadata.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Unique identifier assigned for each item.
        /// </summary>
        public Guid SystemId { get; set; }

        /// <summary>
        /// <see cref="SystemId"/> of parent item. If equals <see cref="Guid.Empty"/>, parent item does note exist.
        /// </summary>
        public Guid Owner { get; set; }

        /// <summary>
        /// The storage identifier is used to retrieve the actual data using <see cref="FileApi"/>'s methods.
        /// </summary>
        public string StorageId { get; set; }

        /// <summary>
        /// Whether the item's data is stored and available to be viewed and downloaded or not.
        /// </summary>
        public bool InStore { get; set; }
        
        /// <summary>
        /// Size of the actual data in bytes.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// The minimum access level required to access the item data.
        /// </summary>
        public AccessLevel AccessLevel { get; set; }

        /// <summary>
        /// The low-level type representaton of the item's data type.
        /// </summary>
        public DataType Type { get; set; }
        
        /// <summary>
        /// The high-level type representaton of the item's data type.
        /// </summary>
        public MediaType MediaType { get; set; }

        /// <summary>
        /// Date when the item was indexed at server.
        /// </summary>
        public DateTime Added { get; set; }

        /// <summary>
        /// The date of the original record if available, othwise same as <see cref="Added"/>.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Name or title of the item.
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// Describes contents of the item. Rarely used.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Represents relevancy of the found item with integer between 0-100, from least to the most relevant. This is calculated by presence of certain weighted keywords which are hardcoded at server. 
        /// <para>See: <seealso cref="ItemApi.ExplainXScoreAsync(Item, System.Threading.CancellationToken)"/></para>
        /// </summary>
        public int XScore { get; set; }

        /// <summary>
        /// Similarity hash called SimHash developed by Moses Charikar. 
        /// <para>Can be used to find similar items when used as a term in <see cref="SearchApi{TResult}.SearchAsync(string, string[], int, int, DateTime?, DateTime?, SortType, MediaType, Guid[], System.Threading.CancellationToken)"/></para>
        /// </summary>
        public ulong Simhash { get; set; }

        /// <summary>
        /// The bucket in which the item resides.
        /// </summary>
        public string Bucket { get; set; }
        
        public KeyValue[] KeyValues { get; set; }
        public string IndexFile { get; set; }
        public string HistoryFile { get; set; }
        public bool PerfectMatch { get; set; }
        public string Group { get; set; }

        [JsonPropertyName("tagsh")]
        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<Friend> Friends { get; set; }

        /// <summary>
        /// Identifiers of the item's related to this item and their relation.
        /// </summary>
        public IEnumerable<Relationship> Relations { get; set; }
    }
}
