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

        public async Task<IReadOnlyCollection<PersonVersion>> GetCurrentAsync()
        {
            var res = _personVersionClient.GetAllCurrent(new GetAllCurrentRequest());
            var sel = res.ResponseStream.ReadAllAsync().Select(x => _mapper.Map<Model.PersonVersion>(x));
            return await sel.ToListAsync();
        }

        public async Task<PersonVersion> GetCurrentByAvIdAsync(int avId)
        {
            var res = _personVersionClient.GetAllByAvId(new GetAllByAvIdRequest() { AvId = avId });
            var rply = await res.ResponseStream.ReadAllAsync().LastAsync();
            var pv = _mapper.Map<Model.PersonVersion>(rply);
            return pv;
        }
    }
}
