using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace AV.AvA.BackupImporter.Commands
{
    [Command(Name = "AV.AvA.BackupImporter", Description = "Imports JSON backups into StorageBackend's database.")]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    internal class MainCommand : BaseCommand
    {
        private static string GetVersion()
            => Versioning.GetVersion();
    }
}
