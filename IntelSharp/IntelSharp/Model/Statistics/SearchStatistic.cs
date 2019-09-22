using System.Collections.Generic;
using IntelSharp.Model.Statistics;

namespace IntelSharp.Model
{
    public class SearchStatistic
    {
        public IEnumerable<DateStatistic> Date { get; set; }
        public IEnumerable<TypeStatistic> Type { get; set; }
        public IEnumerable<MediaStatistic> Media { get; set; }
        public IEnumerable<BucketStatistic> Bucket { get; set; }
        public Dictionary<string, int> Heatmap { get; set; }
        public int Total { get; set; }
        public int Status { get; set; }
        public bool Terminated { get; set; }
    }
}
