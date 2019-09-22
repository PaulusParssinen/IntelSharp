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
        public Guid SystemId { get; set; }
        public Guid Owner { get; set; }
        public string StorageId { get; set; }
        public bool InStore { get; set; }
        public long Size { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public DataType Type { get; set; }
        public DateTime Added { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int XScore { get; set; }
        public ulong Simhash { get; set; }
        public string Bucket { get; set; }
        public KeyValue[] KeyValues { get; set; }
        public string IndexFile { get; set; }
        public string HistoryFile { get; set; }
        public bool PerfectMatch { get; set; }

        [JsonPropertyName("tagsh")]
        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<Relationship> Relationships { get; set; }
    }
}
