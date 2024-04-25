using System.Text.Json;
using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK
{
    [JsonConverter(typeof(SubscriberCustomFieldConverter))]
    public class SubscriberCustomField
    {
        public string? CustomFieldID { get; set; }
        public required string Name { get; set; }
        public object? Value { get; set; }
        [JsonIgnore]
        internal bool IsDateField { get; set; }

        public static implicit operator SubscriberCustomField((string, object?) tuple)
        {
            return new SubscriberCustomField()
            {
                Name = tuple.Item1,
                Value = tuple.Item2,
            };
        }
    }

    internal class InternalSubscriberCustomField
    {
        public string? CustomFieldID { get; set; }
        public required string Name { get; set; }
        public JsonElement Value { get; set; }
    }
}