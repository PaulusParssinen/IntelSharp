using System.Collections.Generic;

namespace IntelSharp.Model
{
    public class SearchResultResponse
    {
        public IEnumerable<Item> Records { get; set; }
        public int Status { get; set; }
    }
}
