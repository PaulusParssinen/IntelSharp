using System.Text.Json.Serialization;

namespace IntelSharp.Model
{
    public class Path
    {
        [JsonPropertyName("path")]
        public string PathUrl { get; set; }

        public int Credit { get; set; }
        public int CreditMax { get; set; }
        public int CreditReset { get; set; }
    }
}
