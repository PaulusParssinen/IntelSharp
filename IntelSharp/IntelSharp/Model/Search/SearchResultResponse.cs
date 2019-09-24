using System.Collections.Generic;

namespace IntelSharp.Model
{
    public class SearchResultResponse
    {
        public IEnumerable<Item> Records { get; set; }
        public SearchResultStatus Status { get; set; }
    }
}
