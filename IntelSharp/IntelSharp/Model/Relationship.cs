using System;

namespace IntelSharp.Model
{
    /// <summary>
    /// Represents relationship between two <see cref="Item"/>s.
    /// </summary>
    public class Relationship
    {
        /// <summary>
        /// Target <see cref="Item.SystemId"/>
        /// </summary>
        public Guid Target { get; set; }
        public int Relation { get; set; }
    }
}
