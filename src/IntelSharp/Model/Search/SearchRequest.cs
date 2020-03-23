using System;
using System.Collections.Generic;

namespace IntelSharp.Model.Search
{
    public class SearchRequest
    {
        public string Term { get; set; }
        public IEnumerable<string> Buckets { get; set; }
        public int Timeout { get; set; }
        public int MaxResults { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public SortType Sort { get; set; }
        public MediaType Media { get; set; }
        public IEnumerable<Guid> Terminate { get; set; }
    }
}
