using System.Collections.Generic;

namespace IntelSharp.Model.Search
{
    public class PhoneBookSearchResults
    {
        public IEnumerable<PhoneBookSelector> Selectors { get; set; }
        public SearchResultStatus Status { get; set; }
    }
}
