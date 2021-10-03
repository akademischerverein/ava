using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV.AvA.Data;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.EntityFrameworkCore;

namespace AV.AvA.BackupTool.Commands
{
    internal class TruncateCommand : BaseCommand
    {
        private readonly AvADbContext _db;

        public TruncateCommand(AvADbContext db)
        {
            _db = db;
        }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            await _db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE person_versions RESTART IDENTITY;");
            return 0;
        }
    }
}
