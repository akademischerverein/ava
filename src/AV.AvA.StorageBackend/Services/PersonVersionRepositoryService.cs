using System.Text.Json;
using AV.AvA.Data;
using AV.AvA.Model;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using NodaTime.Serialization.Protobuf;
using Status = Grpc.Core.Status;

namespace AV.AvA.StorageBackend.Services
{
    public class PersonVersionRepositoryService : PersonVersionRepository.PersonVersionRepositoryBase
    {
        private readonly ILogger<PersonVersionRepositoryService> _l;
        private readonly AvADbContext _dbContext;

        public PersonVersionRepositoryService(ILogger<PersonVersionRepositoryService> logger, AvADbContext dbContext)
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
            var personVersions = _dbContext.GetCurrentPersonVersions().Where(p => p.PersonVersionId > request.HighestKnownPersonVersionId);
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

        public override async Task GetAllByAvId(GetAllByAvIdRequest request, IServerStreamWriter<PersonVersionReply> responseStream, ServerCallContext context)
        {
            var personVersions = _dbContext.PersonVersions.Where(p => p.PersonVersionId >= request.StartWithPersonVersionId && p.AvId == request.AvId);
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

        public override async Task<CreateNewPersonVersionReply> CreateNewPersonVersion(CreateNewPersonVersionRequest request, ServerCallContext context)
        {
            if (!_dbContext.PersonVersions.Any(p => p.AvId == request.AvId))
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Unknown AvId"));
            }

            _dbContext.Database.BeginTransaction();
            _dbContext.Database.ExecuteSqlRaw("LOCK TABLE person_versions IN ACCESS EXCLUSIVE;");
            var pvId = _dbContext.PersonVersions.Max(p => p.PersonVersionId);
            if (pvId <= 0)
            {
                pvId = 1;
            }

            var pv = new Model.PersonVersion
            {
                AvId = request.AvId,
                ComitterAvId = null, // TODO
                CommitMessage = request.CommitMessage,
                Person = JsonSerializer.Deserialize<Person>(request.Person) ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "No person data found")),
                PersonVersionId = pvId,
                Software = request.Software,
            };
            _dbContext.PersonVersions.Add(pv);
            await _dbContext.Database.CommitTransactionAsync();

            var retPv = await _dbContext.PersonVersions.Where(p => p.PersonVersionId == pvId).FirstAsync();
            return new CreateNewPersonVersionReply
            {
                CommittedAt = retPv.CommittedAt.ToTimestamp(),
                PersonVersionId = retPv.PersonVersionId,
            };
        }

        public override async Task<CreateNewPersonReply> CreateNewPerson(CreateNewPersonRequest request, ServerCallContext context)
        {
            _dbContext.Database.BeginTransaction();
            _dbContext.Database.ExecuteSqlRaw("LOCK TABLE person_versions IN ACCESS EXCLUSIVE;");
            var pvId = _dbContext.PersonVersions.Max(p => p.PersonVersionId);
            if (pvId <= 0)
            {
                pvId = 1;
            }

            var avId = _dbContext.PersonVersions.Max(p => p.AvId);
            if (avId <= 0)
            {
                avId = 100104;
            }
            else
            {
                avId += 97;
            }

            var pv = new Model.PersonVersion
            {
                AvId = avId,
                ComitterAvId = null, // TODO
                CommitMessage = request.CommitMessage,
                Person = JsonSerializer.Deserialize<Person>(request.Person) ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "No person data found")),
                PersonVersionId = pvId,
                Software = request.Software,
            };
            _dbContext.PersonVersions.Add(pv);
            await _dbContext.Database.CommitTransactionAsync();

            var retPv = await _dbContext.PersonVersions.Where(p => p.PersonVersionId == pvId).FirstAsync();
            return new CreateNewPersonReply
            {
                AvId = retPv.AvId,
                CommittedAt = retPv.CommittedAt.ToTimestamp(),
                PersonVersionId = retPv.PersonVersionId,
            };
        }
    }
}
