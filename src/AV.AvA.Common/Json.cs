using System.Text.Json;
using System.Text.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace AV.AvA.Common
{
    public static class Json
    {
        public static JsonSerializerOptions CreateSTJOptions()
        {
            var x = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                Converters =
                    {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
                    },
            };
            x.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            return x;
        }
    }
}
