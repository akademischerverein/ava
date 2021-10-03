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
    internal class ExportCommand : BaseCommand
    {
        private readonly AvADbContext _db;
        private readonly JsonSerializerOptions _jsonOpts;

        public ExportCommand(AvADbContext db, JsonSerializerOptions jsonOpts)
        {
            _db = db;
            _jsonOpts = jsonOpts;
        }

        [Argument(0, Description = "Path to JSON backup.")]
        [LegalFilePath]
        [Required]
        public string JsonBackupPath { get; set; } = default!;

        [Option(
            Description = "Don't indent/pretty-print JSON output. Creates smaller but functional equal files that are less human-readable.",
            ShortName = "ni")]
        public bool DontIndentOutput { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            var jsonOpts = new JsonSerializerOptions(_jsonOpts)
            {
                WriteIndented = !DontIndentOutput,
            };

            using var fs = new FileStream(JsonBackupPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            using var tran = await _db.Database.BeginTransactionAsync();
            var lst = await _db.PersonVersions.OrderBy(pv => pv.PersonVersionId).ToListAsync();
            var proj = lst.Select(x => new PersonVersion()
            {
                AvId = x.AvId,
                ComitterAvId = x.ComitterAvId,
                CommitMessage = x.CommitMessage,
                CommittedAt = x.CommittedAt,
                PersonVersionId = x.PersonVersionId,
                Software = x.Software,
                Person = JsonSerializer.Deserialize<Person>(x.Person, jsonOpts)
                    ?? throw new InvalidDataException($"Person data in row with {nameof(x.PersonVersionId)} {x.PersonVersionId}" +
                    " not be deserialized properly."),
            });
            await JsonSerializer.SerializeAsync(fs, proj, jsonOpts);
            await tran.RollbackAsync();

            return 0;
        }
    }
}
