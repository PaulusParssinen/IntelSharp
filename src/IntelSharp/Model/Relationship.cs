using System;

namespace IntelSharp.Model
{
    /// <summary>
    /// Represents a relationship between two <see cref="Item">items.</see>
    /// </summary>
    public class Relationship
    {
        /// <summary>
        /// Represents the target item's <see cref="Item.SystemId"/>
        /// </summary>
        public Guid Target { get; set; }
        public int Relation { get; set; }
    }
}
