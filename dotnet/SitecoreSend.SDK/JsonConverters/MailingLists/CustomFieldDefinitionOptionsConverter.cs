using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace SitecoreSend.SDK.JsonConverters
{
    public class CustomFieldDefinitionOptionsConverter : JsonConverter<IList<string>>
    {
        public override IList<string>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (string.IsNullOrEmpty(value))
            {
                return [];
            }
            var doc = XDocument.Parse(value);

            return doc.Descendants("item")
                .Select(item => item.Element("value")?.Value).Where(x => x != null)!.ToList<string>();
        }

        public override void Write(Utf8JsonWriter writer, IList<string> value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(string.Join(",", value));
        }
    }
}