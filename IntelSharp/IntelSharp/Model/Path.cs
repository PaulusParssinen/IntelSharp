using System.Text.Json.Serialization;

namespace IntelSharp.Model
{
    public class Path
    {
        [JsonPropertyName("path")]
        public string PathUrl { get; set; }

        /// <summary>
        /// Credits left
        /// </summary>
        public int Credit { get; set; }

        public int CreditMax { get; set; }

        /// <summary>
        /// Days between the credit resets.
        /// </summary>
        public int CreditReset { get; set; }
    }
}
