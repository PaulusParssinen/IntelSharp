using System;

namespace IntelSharp.Model
{
    public class SearchResponse
    {
        public Guid Id { get; set; }
        public bool SoftSelectorWarning { get; set; }
        public SearchStatus Status { get; set; }
    }
}
