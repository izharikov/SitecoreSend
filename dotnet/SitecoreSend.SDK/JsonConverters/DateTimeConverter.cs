using System.Text.Json;
using System.Text.Json.Serialization;
using SitecoreSend.SDK.Tools;

namespace SitecoreSend.SDK.JsonConverters
{
    public class DateTimeConverter : JsonConverter<DateTimeOffset>
    {
        public override bool HandleNull => false;

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            return DateTimeTools.Parse(dateString);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            
        }
    }
}