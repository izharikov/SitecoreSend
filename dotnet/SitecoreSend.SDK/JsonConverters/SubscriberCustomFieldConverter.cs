using System.Text.Json;
using System.Text.Json.Serialization;
using SitecoreSend.SDK.Tools;

namespace SitecoreSend.SDK.JsonConverters
{
    public class SubscriberCustomFieldConverter : JsonConverter<SubscriberCustomField>
    {
        public override SubscriberCustomField? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var elem = JsonSerializer.Deserialize<InternalSubscriberCustomField>(ref reader, options);
            if (elem == null)
            {
                return null;
            }

            return new SubscriberCustomField()
            {
                CustomFieldID = elem.CustomFieldID,
                Name = elem.Name,
                Value = ParseValue(elem.Value, out var isDate),
                IsDateField = isDate,
            };
        }

        public override void Write(Utf8JsonWriter writer, SubscriberCustomField? field, JsonSerializerOptions options)
        {
            var value = field?.Value?.ToString();
            if (field?.Value is DateTimeOffset date)
            {
                value = date.ToString("yyyy-MM-dd");
            }
            writer.WriteStringValue(field != null ? $"{field.Name}={value}" : string.Empty);
        }

        private static object? ParseValue(JsonElement element, out bool isDate)
        {
            isDate = false;
            if (element.ValueKind == JsonValueKind.Number)
            {
                if (element.TryGetInt32(out var intValue))
                {
                    return intValue;
                }
            }

            var rawValue = element.GetString();
            if (DateTimeTools.IsDate(rawValue))
            {
                isDate = true;
                return DateTimeTools.Parse(rawValue);
            }

            return rawValue;
        }
    }
}