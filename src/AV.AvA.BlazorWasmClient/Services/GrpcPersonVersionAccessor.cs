using AutoMapper;
using AV.AvA.Model;
using Grpc.Core;

namespace AV.AvA.BlazorWasmClient.Services
{
    public class GrpcPersonVersionAccessor : IPersonVersionAccessor
    {
        private readonly PersonVersionRepository.PersonVersionRepositoryClient _personVersionClient;
        private readonly IMapper _mapper;

        public GrpcPersonVersionAccessor(PersonVersionRepository.PersonVersionRepositoryClient personVersionClient, IMapper mapper)
        {
            _personVersionClient = personVersionClient;
            _mapper = mapper;
        }

        public async Task<PersonVersion> GetCurrentAsync(int avId)
        {
            var res = _personVersionClient.GetAllByAvId(new GetAllByAvIdRequest() { AvId = avId });
            var rply = await res.ResponseStream.ReadAllAsync().LastAsync();
            var pv = _mapper.Map<Model.PersonVersion>(rply);
            return pv;
        }
    }
}
