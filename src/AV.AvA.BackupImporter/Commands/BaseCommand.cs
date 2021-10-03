using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace AV.AvA.BackupImporter.Commands
{
    [HelpOption("--help")]
    internal class BaseCommand
    {
        protected virtual Task<int> OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return Task.FromResult(1);
        }
    }
}
