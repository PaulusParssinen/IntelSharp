using System.Collections.Generic;

namespace IntelSharp.Model.Search
{
    public class IntelligentSearchResults
    {
        public IEnumerable<Item> Records { get; set; }
        public SearchResultStatus Status { get; set; }
    }
}
