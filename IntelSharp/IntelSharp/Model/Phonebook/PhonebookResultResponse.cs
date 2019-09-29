using System.Collections.Generic;

namespace IntelSharp.Model
{
    public class PhonebookResultResponse
    {
        public IEnumerable<PhonebookSelector> Selectors { get; set; }
        public SearchResultStatus Status { get; set; }
    }
}
