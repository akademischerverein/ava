using System.Text.Json;
using AV.AvA.Data;
using AV.AvA.Model;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using NodaTime.Serialization.Protobuf;
using Status = Grpc.Core.Status;
using StorageModel = AV.AvA.StorageModel;

namespace AV.AvA.StorageBackend.Services
{
    public class PersonVersionRepositoryService : PersonVersionRepository.PersonVersionRepositoryBase
    {
        private readonly ILogger<PersonVersionRepositoryService> _l;
        private readonly AvADbContext _dbContext;
        private readonly JsonSerializerOptions _jsonOpts;

        public PersonVersionRepositoryService(ILogger<PersonVersionRepositoryService> logger, AvADbContext dbContext, JsonSerializerOptions jsonOpts)
        {
            _l = logger;
            _dbContext = dbContext;
            _jsonOpts = jsonOpts;
        }

        public override Task GetAll(GetAllRequest request, IServerStreamWriter<PersonVersionReply> responseStream, ServerCallContext ctx)
        {
            var personVersions = _dbContext.PersonVersions.Where(p => p.PersonVersionId > request.HighestKnownPersonVersionId);
            return YieldToResponseStream(personVersions, responseStream);
        }

        public override Task GetAllCurrent(GetAllCurrentRequest request, IServerStreamWriter<PersonVersionReply> responseStream, ServerCallContext context)
        {
            var personVersions = _dbContext.GetCurrentPersonVersions().Where(p => p.PersonVersionId > request.HighestKnownPersonVersionId);
            return YieldToResponseStream(personVersions, responseStream);
        }

        public override Task GetAllByAvId(GetAllByAvIdRequest request, IServerStreamWriter<PersonVersionReply> responseStream, ServerCallContext context)
        {
            var personVersions = _dbContext.PersonVersions.Where(p => p.PersonVersionId >= request.StartWithPersonVersionId && p.AvId == request.AvId);
            return YieldToResponseStream(personVersions, responseStream);
        }

        public override async Task<CreateNewPersonVersionReply> CreateNewPersonVersion(CreateNewPersonVersionRequest request, ServerCallContext context)
        {
            EnsurePersonDeserializable(request.Person);

            var any = await _dbContext.PersonVersions.AnyAsync(p => p.AvId == request.AvId);
            if (!any)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Unknown AvId"));
            }

            var pv = new StorageModel.PersonVersion
            {
                AvId = request.AvId,
                ComitterAvId = null, // TODO
                CommitMessage = request.CommitMessage,
                Person = request.Person,
                Software = request.Software,
            };
            _dbContext.PersonVersions.Add(pv);
            await _dbContext.SaveChangesAsync();

            return new CreateNewPersonVersionReply
            {
                CommittedAt = pv.CommittedAt.ToTimestamp(),
                PersonVersionId = pv.PersonVersionId,
            };
        }

        public override async Task<CreateNewPersonReply> CreateNewPerson(CreateNewPersonRequest request, ServerCallContext context)
        {
            EnsurePersonDeserializable(request.Person);

            using var tran = await _dbContext.Database.BeginTransactionAsync();
            await _dbContext.Database.ExecuteSqlRawAsync("LOCK TABLE person_versions IN ACCESS EXCLUSIVE MODE;");

            var avId = await _dbContext.PersonVersions.MaxAsync(p => (int?)p.AvId) ?? 100104 - 97;
            avId += 97;

            var pv = new StorageModel.PersonVersion
            {
                AvId = avId,
                ComitterAvId = null, // TODO
                CommitMessage = request.CommitMessage,
                Person = request.Person,
                Software = request.Software,
            };
            _dbContext.PersonVersions.Add(pv);
            await _dbContext.SaveChangesAsync();
            await tran.CommitAsync();

            return new CreateNewPersonReply
            {
                AvId = avId,
                CommittedAt = pv.CommittedAt.ToTimestamp(),
                PersonVersionId = pv.PersonVersionId,
            };
        }

        private static PersonVersionReply MapToReply(StorageModel.PersonVersion pv) => new()
        {
            PersonVersionId = pv.PersonVersionId,
            AvId = pv.AvId,
            ComitterAvId = pv.ComitterAvId,
            CommitMessage = pv.CommitMessage,
            CommittedAt = pv.CommittedAt.ToTimestamp(),
            Person = pv.Person,
            Software = pv.Software,
        };

        private static async Task YieldToResponseStream(IQueryable<StorageModel.PersonVersion> q, IServerStreamWriter<PersonVersionReply> responseStream)
        {
            await foreach (var p in q.AsAsyncEnumerable())
            {
                await responseStream.WriteAsync(MapToReply(p));
            }
        }

        private void EnsurePersonDeserializable(string json)
        {
            try
            {
                var testDeser = JsonSerializer.Deserialize<Person>(json, _jsonOpts);
            }
            catch (JsonException je)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "no valid person json given", je));
            }
        }
    }
}
