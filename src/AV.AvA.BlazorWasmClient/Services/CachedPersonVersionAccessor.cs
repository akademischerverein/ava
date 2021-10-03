using AutoMapper;
using AV.AvA.Model;
using Grpc.Core;
using Microsoft.Extensions.Caching.Memory;
using NodaTime;

namespace AV.AvA.BlazorWasmClient.Services
{
    public class CachedPersonVersionAccessor : IPersonVersionAccessor
    {
        private const string CacheKey = "7559ed02-ab76-4316-93bc-d755302972e6";
        private const int CacheMaxMinutes = 60 * 4; // 4h
        private readonly PersonVersionRepository.PersonVersionRepositoryClient _personVersionClient;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public CachedPersonVersionAccessor(
            PersonVersionRepository.PersonVersionRepositoryClient personVersionClient,
            IMapper mapper,
            IMemoryCache memoryCache)
        {
            _personVersionClient = personVersionClient;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<IReadOnlyCollection<PersonVersion>> GetCurrentAsync()
        {
            var cache = await GetPersonsCached();
            return cache.Values;
        }

        public async Task<PersonVersion> GetCurrentByAvIdAsync(int avId)
        {
            var cache = await GetPersonsCached();
            return cache[avId];
        }

        private async Task<Dictionary<int, PersonVersion>> GetPersonsCached()
        {
            return await _memoryCache.GetOrCreateAsync(CacheKey, async ce =>
            {
                ce.SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheMaxMinutes));
                var lst = await GetAllCurrentFromBackend();
                var dic = lst.ToDictionary(x => x.AvId, x => x);
                return dic;
            });
        }

        private async Task<List<PersonVersion>> GetAllCurrentFromBackend()
        {
            var res = _personVersionClient.GetAllCurrent(new GetAllCurrentRequest());
            var sel = res.ResponseStream.ReadAllAsync().Select(x => _mapper.Map<Model.PersonVersion>(x));
            return await sel.ToListAsync();
        }
    }
}
