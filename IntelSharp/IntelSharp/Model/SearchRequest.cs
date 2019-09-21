using System;

namespace IntelSharp.Model
{
    public class SearchRequest
    {
        //lookuplevel
        public string Term { get; set; }
        public string[] Buckets { get; set; }
        public int Timeout { get; set; }
        public int MaxResults { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public SortType Sort { get; set; }
        public MediaType Media { get; set; }
        public Guid[] Terminate { get; set; }
    }
}
