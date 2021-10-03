using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace AV.AvA.BackupTool.Commands
{
    [Command(Name = "AV.AvA.BackupTool", Description = "Imports/exports JSON backups into/from StorageBackend's database.")]
    [Subcommand(typeof(ImportCommand), typeof(ExportCommand), typeof(TruncateCommand))]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    internal class MainCommand : BaseCommand
    {
        private static string GetVersion()
            => Versioning.GetVersion();
    }
}
