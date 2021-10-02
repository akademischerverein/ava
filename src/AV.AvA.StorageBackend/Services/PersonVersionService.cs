using System.Text.Json;
using AV.AvA.Data;
using AV.AvA.StorageBackend;
using Grpc.Core;
using NodaTime.Serialization.Protobuf;

namespace AV.AvA.StorageBackend.Services
{
    public class PersonVersionService : AvA.PersonVersion.PersonVersionBase
    {
        private readonly ILogger<PersonVersionService> _l;
        private readonly AvADbContext _dbContext;

        public PersonVersionService(ILogger<PersonVersionService> logger, AvADbContext dbContext)
        {
            _l = logger;
            _dbContext = dbContext;
        }

        public override async Task GetAll(GetAllRequest request, IServerStreamWriter<PersonVersionReply> responseStream, ServerCallContext ctx)
        {
            var personVersions = _dbContext.PersonVersions.Where(p => p.PersonVersionId > request.HighestKnownPersonVersionId);
            foreach (var p in personVersions)
            {
                var reply = new PersonVersionReply
                {
                    PersonVersionId = p.PersonVersionId,
                    AvId = p.AvId,
                    ComitterAvId = p.ComitterAvId,
                    CommitMessage = p.CommitMessage,
                    CommittedAt = p.CommittedAt.ToTimestamp(),
                    Person = JsonSerializer.Serialize(p.Person),
                    Software = p.Software,
                };
                await responseStream.WriteAsync(reply);
            }
        }

        public override async Task GetAllCurrent(GetAllCurrentRequest request, IServerStreamWriter<PersonVersionReply> responseStream, ServerCallContext context)
        {
            var personVersions = _dbContext.PersonVersions.GroupBy(p => p.AvId).Select(p => p.OrderByDescending())
            foreach (var p in personVersions)
            {
                var reply = new PersonVersionReply
                {
                    PersonVersionId = p.PersonVersionId,
                    AvId = p.AvId,
                    ComitterAvId = p.ComitterAvId,
                    CommitMessage = p.CommitMessage,
                    CommittedAt = p.CommittedAt.ToTimestamp(),
                    Person = JsonSerializer.Serialize(p.Person),
                    Software = p.Software,
                };
                await responseStream.WriteAsync(reply);
            }
        }
    }
}
