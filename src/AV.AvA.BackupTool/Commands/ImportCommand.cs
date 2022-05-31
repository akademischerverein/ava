using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AV.AvA.Data;
using AV.AvA.Model;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.EntityFrameworkCore;

namespace AV.AvA.BackupTool.Commands
{
    internal class ImportCommand : BaseCommand
    {
        private readonly AvADbContext _db;
        private readonly JsonSerializerOptions _jsonOpts;

        public ImportCommand(AvADbContext db, JsonSerializerOptions jsonOpts)
        {
            _db = db;
            _jsonOpts = jsonOpts;
        }

        [Argument(0, Description = "Path to JSON backup.")]
        [LegalFilePath]
        [Required]
        public string JsonBackupPath { get; set; } = default!;

        [Option(
            Description = "Atomically replace person versions instead of appending them",
            ShortName = "r")]
        public bool ReplacePersonVersions { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            using var fs = new FileStream(JsonBackupPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var tran = await _db.Database.BeginTransactionAsync();

            if (ReplacePersonVersions)
            {
                await _db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE person_versions RESTART IDENTITY;");
            }

            await foreach (var pv in JsonSerializer.DeserializeAsyncEnumerable<PersonVersion>(fs, _jsonOpts))
            {
                _ = pv ?? throw new InvalidOperationException("Deserialization yielded a null PersonVersion");
                _db.PersonVersions.Add(new StorageModel.PersonVersion
                {
                    AvId = pv.AvId,
                    ComitterAvId = pv.ComitterAvId,
                    CommitMessage = pv.CommitMessage,
                    CommittedAt = pv.CommittedAt,
                    Software = pv.Software,
                    Person = JsonSerializer.Serialize(pv.Person, _jsonOpts),
                    PersonVersionId = pv.PersonVersionId,
                });
            }

            await _db.SaveChangesAsync();
            await tran.CommitAsync();

            return 0;
        }
    }
}
