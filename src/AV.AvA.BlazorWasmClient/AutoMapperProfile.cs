using System.Text.Json;
using AutoMapper;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Model = AV.AvA.Model;

namespace AV.AvA.BlazorWasmClient
{
    public class AutoMapperProfile : Profile
    {
        private static readonly JsonSerializerOptions _jsonOpt = GetNodaTimeSTJSettings();

        public AutoMapperProfile()
        {
            CreateMap<PersonVersionReply, Model.PersonVersion>()
                .ForMember(
                    dest => dest.Person,
                    opt => opt.MapFrom((src, dest) =>
                        JsonSerializer.Deserialize<Model.Person>(src.Person, _jsonOpt)))
                .ForMember(
                    dest => dest.CommittedAt,
                    opt => opt.MapFrom((src, dest) =>
                        Instant.FromDateTimeOffset(src.CommittedAt.ToDateTimeOffset())));
        }

        private static JsonSerializerOptions GetNodaTimeSTJSettings()
        {
            var x = new JsonSerializerOptions();
            x.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            return x;
        }
    }
}
