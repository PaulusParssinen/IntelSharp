using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IntelSharp.Json
{
    public class DateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.GetDateTime();

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.HasValue ? 
                string.Empty : value.Value.ToString("yyyy-MM-dd HH:mm"));
        }
    }
}
