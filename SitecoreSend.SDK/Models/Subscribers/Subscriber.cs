using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK
{
    public class Subscriber
    {
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public required string Email { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTimeOffset CreatedOn { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTimeOffset? UnsubscribedOn { get; set; }

        public Guid? UnsubscribedFromID { get; set; }
        public SubscriberStatus SubscribeType { get; set; }
        public SubscribeMethod? SubscribeMethod { get; set; }
        public List<SubscriberCustomField> CustomFields { get; set; } = [];

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTimeOffset? RemovedOn { get; set; }
        public List<string> Tags { get; set; } = [];

        public object? this[string name]
        {
            get
            {
                return CustomFields.FirstOrDefault(x => x.Name == name)?.Value;
            }
            set
            {
                var field = CustomFields.FirstOrDefault(x => x.Name == name);
                if (field == null)
                {
                    field = new SubscriberCustomField()
                    {
                        Name = name,
                    };
                    CustomFields.Add(field);
                }

                field.Value = value;
            }
        }

        public static implicit operator SubscriberRequest(Subscriber s) => new SubscriberRequest()
        {
            Email = s.Email,
            Name = s.Name,
            CustomFields = s.CustomFields,
            Tags = s.Tags,
        };
    }
}