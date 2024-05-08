using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK;

public class CustomFieldDefinition
{
    public Guid ID { get; set; }
    public required string Name { get; set; }
    public bool IsRequired { get; set; }
    public CustomFieldType Type { get; set; }
    [JsonConverter(typeof(CustomFieldDefinitionOptionsConverter))]
    [JsonPropertyName("Context")]
    public IList<string> Options { get; set; } = [];
    public bool IsHidden { get; set; }
    public string? FallBackValue { get; set; }
}

public class CustomFieldDefinitionRequest
{
    public required string Name { get; set; }
    public bool IsRequired { get; set; }
    public CustomFieldType CustomFieldType { get; set; }
    [JsonConverter(typeof(CustomFieldDefinitionOptionsConverter))]
    public IList<string> Options { get; set; } = [];
    public bool IsHidden { get; set; }
    public string? FallBackValue { get; set; }
}