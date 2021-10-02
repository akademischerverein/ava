using System.Text.Json;
using AV.AvA.Model;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace AV.AvA.StorageBackend
{
    internal static class Extensions
    {
        private static readonly JsonSerializerOptions _jsonOpt = GetNodaTimeSTJSettings();

        public static Person DeserializePerson(this StorageModel.PersonVersion pv) =>
            JsonSerializer.Deserialize<Person>(pv.Person, _jsonOpt)
            ?? throw new ArgumentException("Given PersonVersion does not contain a valid Person JSON representation", nameof(pv));

        private static JsonSerializerOptions GetNodaTimeSTJSettings()
        {
            var x = new JsonSerializerOptions();
            x.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            return x;
        }
    }
}
