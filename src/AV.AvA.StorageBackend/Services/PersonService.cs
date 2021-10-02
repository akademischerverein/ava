using AV.AvA.StorageBackend;
using Grpc.Core;

namespace AV.AvA.StorageBackend.Services
{
    public class PersonService : AvA.Person.PersonBase
    {
        private readonly ILogger<PersonService> _l;

        public PersonService(ILogger<PersonService> logger)
        {
            _l = logger;
        }
    }
}
