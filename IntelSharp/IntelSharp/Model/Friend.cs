using System;

namespace IntelSharp.Model
{
    /// <summary>
    /// Represents friend relationship between two <see cref="Item"/>s.
    /// </summary>
    public class Friend
    {
        public Guid SystemId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Represents inline shadow copy of the <see cref="Item"/> metadata.
        /// </summary>
        public Item Inline { get; set; }
    }
}
