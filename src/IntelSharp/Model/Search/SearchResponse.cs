using System;
using System.Text.Json.Serialization;

namespace IntelSharp.Model.Search
{
    public class SearchResponse
    {
        public Guid Id { get; set; }
        public bool SoftSelectorWarning { get; set; }
        public SearchStatus Status { get; set; }

        [JsonPropertyName("altterm")]
        public string AlternativeTerm { get; set; }
    }
}
