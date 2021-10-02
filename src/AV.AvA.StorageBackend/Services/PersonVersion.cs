using AV.AvA.StorageBackend;
using Grpc.Core;

namespace AV.AvA.StorageBackend.Services
{
    public class PersonVersion : AvA.PersonVersion.PersonVersionBase
    {
        private readonly ILogger<PersonVersion> _l;

        public PersonVersion(ILogger<PersonVersion> logger)
        {
            _l = logger;
        }
    }
}
