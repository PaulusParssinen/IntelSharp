using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IntelSharp.Json
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (value == default)
                writer.WriteStringValue(string.Empty);
            else writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm"));
        }
    }
}
