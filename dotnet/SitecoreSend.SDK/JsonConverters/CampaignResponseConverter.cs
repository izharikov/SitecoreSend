using System.Text.Json;
using System.Text.Json.Serialization;

namespace SitecoreSend.SDK.JsonConverters;

public class CampaignResponseConverter: JsonConverter<CampaignResponse>
{
    public override CampaignResponse? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            var model = JsonSerializer.Deserialize<CampaignResponseInternal>(ref reader, options);
            return model;
        }

        var id = reader.GetGuid();
        return new CampaignResponse()
        {
            ID = id,
        };
    }

    public override void Write(Utf8JsonWriter writer, CampaignResponse value, JsonSerializerOptions options)
    {
        
    }
}