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

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            using var fs = new FileStream(JsonBackupPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            using var tran = await _db.Database.BeginTransactionAsync();
            var lst = await _db.PersonVersions.OrderBy(pv => pv.PersonVersionId).ToListAsync();
            await JsonSerializer.SerializeAsync(fs, lst, _jsonOpts);
            await tran.RollbackAsync();

            return 0;
        }
    }
}
