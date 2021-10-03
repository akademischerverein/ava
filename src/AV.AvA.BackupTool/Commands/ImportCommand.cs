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

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            using var fs = new FileStream(JsonBackupPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var tran = await _db.Database.BeginTransactionAsync();
            await foreach (var pv in JsonSerializer.DeserializeAsyncEnumerable<PersonVersion>(fs, _jsonOpts))
            {
                _ = pv ?? throw new InvalidOperationException("Deserialization yielded a null PersonVersion");
                _db.PersonVersions.Add(new StorageModel.PersonVersion
                {
                    AvId = pv.AvId,
                    ComitterAvId = pv.ComitterAvId,
                    CommitMessage = pv.CommitMessage,
                    CommittedAt = pv.CommittedAt,
                    Person = JsonSerializer.Serialize(pv.Person, _jsonOpts),
                    Software = pv.Software,
                });
            }

            await _db.SaveChangesAsync();
            await tran.CommitAsync();

            return 0;
        }
    }
}
