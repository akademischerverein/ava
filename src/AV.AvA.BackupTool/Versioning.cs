using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AV.AvA.BackupTool
{
    internal static class Versioning
    {
        public static string GetVersion() =>
            Assembly
                .GetEntryAssembly()
                ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion
            ?? throw new InvalidOperationException("The EntryAssembly or its AssemblyInformationalVersionAttribute could not be retreived.");

        public static string GetPrettyVersion()
        {
            var v = GetVersion();

            // we assume v is SemVer/by nbgv, so either i.e.
            // 0.1.2-alpha+asdf or 0.1.2+asdf
            if (v.Contains("-"))
            {
                return v;
            }

            var plusIdx = v.IndexOf("+");
            if (plusIdx > 0)
            {
                return v.Substring(0, plusIdx); // 0.1.2 instead of 0.1.2+asdf
            }

            return v;
        }
    }
}
