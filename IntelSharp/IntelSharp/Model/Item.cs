using System;
using System.Collections.Generic;

namespace IntelSharp.Model
{
    /// <summary>
    /// Represents the item's metadata.
    /// </summary>
    public class Item
    {
        public string SystemId { get; set; }
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
        public long Simhash { get; set; }
        public string Bucket { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Relationship> Relationships { get; set; }
    }
}
