using AV.AvA.Model;
using NodaTime;

namespace AV.AvA.BlazorWasmClient.Models
{
    public class DefaultXlsxExportModel
    {
        public int AvId { get; set; }
        public Geschlecht Geschlecht { get; set; }

        public string? PraefixTitel { get; set; }

        public string Vorname { get; set; } = default!;

        public string? Zweitnamen { get; set; }

        /// <summary>
        /// List<string> in Person.
        /// </summary>
        public string? Spitznamen { get; set; }

        public string? NachnamePraefix { get; set; }

        public string Nachname { get; set; } = default!;

        public string? SuffixTitel { get; set; }

        public string? Geburtsname { get; set; }

        public string? GeburtsnamePraefix { get; set; }

        public string? Geburtsort { get; set; }

        public DateTime? Geburtsdatum { get; set; }

        public string? Kolonie { get; set; }

        public int? Debitorennummer { get; set; }

        public string? Webseite { get; set; }
    }
}
