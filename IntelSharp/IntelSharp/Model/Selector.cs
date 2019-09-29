using System;
using System.Text.Json.Serialization;

namespace IntelSharp.Model
{
    public class Selector
    {
        public Guid SystemId { get; set; }
        
        [JsonPropertyName("selector")]
        public string Value { get; set; }

        public SelectorType Type { get; set; }
    }
}
