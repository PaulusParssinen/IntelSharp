using System;
using System.Collections.Generic;

namespace IntelSharp.Model
{
    public class AuthenticationInfo
    {
        public DateTime Added { get; set; }
        public string[] Buckets { get; set; }
        public string[] Preview { get; set; }
        public IDictionary<string, Path> Paths { get; set; }
        public int SearchesActive { get; set; }
        public int MaxConcurrentSearches { get; set; }
    }
}
