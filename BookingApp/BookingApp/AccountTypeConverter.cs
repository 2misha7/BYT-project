using System.Text.Json;
using System.Text.Json.Serialization;
using BookingApp.Models;

namespace BookingApp;

public class AccountTypeConverter : JsonConverter<IAccountType>
{
    public override IAccountType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var jsonObject = jsonDoc.RootElement;
            if (jsonObject.TryGetProperty("StartOfSubscription", out _))
            {
                return JsonSerializer.Deserialize<PremiumAccountType>(jsonObject.GetRawText(), options);
            }
            else
            {
                return JsonSerializer.Deserialize<RegularAccountType>(jsonObject.GetRawText(), options);
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, IAccountType value, JsonSerializerOptions options)
    {
        if (value is PremiumAccountType premiumAccount)
        {
            JsonSerializer.Serialize(writer, premiumAccount, options);
        }
        else if (value is RegularAccountType regularAccount)
        {
            JsonSerializer.Serialize(writer, regularAccount, options);
        }
    }
}