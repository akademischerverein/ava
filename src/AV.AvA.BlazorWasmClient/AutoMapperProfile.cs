using System.Text.Json;
using AutoMapper;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Model = AV.AvA.Model;

namespace AV.AvA.BlazorWasmClient
{
    public class AutoMapperProfile : Profile
    {
        private static readonly JsonSerializerOptions _jsonOpt = AV.AvA.Common.Json.CreateSTJOptions();

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

            CreateMap<Model.PersonVersion, Models.DefaultXlsxExportModel>()
                .AfterMap((src, dest, ctx) => ctx.Mapper.Map(src.Person, dest));

            CreateMap<LocalDate, DateTime>().ConvertUsing(s => s.ToDateTimeUnspecified());

            CreateMap<Model.Person, Models.DefaultXlsxExportModel>()
                .ForMember(
                    dest => dest.Spitznamen,
                    opt => opt.MapFrom((src, dest) => string.Join(", ", src.Spitznamen)));
        }
    }
}
