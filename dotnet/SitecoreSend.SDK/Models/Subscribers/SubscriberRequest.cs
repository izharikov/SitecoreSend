namespace SitecoreSend.SDK;

public class SubscriberRequest
{
    public string? Name { get; set; }
    public required string Email { get; set; }
    public bool HasExternalDoubleOptIn { get; set; }
    public IList<SubscriberCustomField> CustomFields { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    public SubscriberStatus SubscribeType { get; set; } = SubscriberStatus.Subscribed;

    public object? this[string name]
    {
        get { return CustomFields.FirstOrDefault(x => x.Name == name)?.Value; }
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
}