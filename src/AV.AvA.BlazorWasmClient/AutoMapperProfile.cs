using System.Text.Json;
using AutoMapper;
using NodaTime;
using Model = AV.AvA.Model;

namespace AV.AvA.BlazorWasmClient
{
    public class AutoMapperProfile : Profile
    {
        private readonly JsonSerializerOptions _jsonOpts;

        public AutoMapperProfile(JsonSerializerOptions jsonOpts)
        {
            CreateMap<PersonVersionReply, Model.PersonVersion>()
                .ForMember(
                    dest => dest.Person,
                    opt => opt.MapFrom((src, dest) =>
                        JsonSerializer.Deserialize<Model.Person>(src.Person, _jsonOpts)))
                .ForMember(
                    dest => dest.CommittedAt,
                    opt => opt.MapFrom((src, dest) =>
                        Instant.FromDateTimeOffset(src.CommittedAt.ToDateTimeOffset())));
            _jsonOpts = jsonOpts;
        }
    }
}
