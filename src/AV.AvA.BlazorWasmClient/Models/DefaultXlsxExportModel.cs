using AV.AvA.Model;
using ClosedXML.Attributes;

namespace AV.AvA.BlazorWasmClient.Models
{
    public class DefaultXlsxExportModel
    {
        [XLColumn(Header = "AV-ID")]
        public int AvId { get; set; }
        public Geschlecht Geschlecht { get; set; }

        [XLColumn(Header = "Präfix Titel")]
        public string? PraefixTitel { get; set; }

        public string Vorname { get; set; } = default!;

        public string? Zweitnamen { get; set; }

        /// <summary>
        /// List<string> in Person.
        /// </summary>
        public string? Spitznamen { get; set; }

        [XLColumn(Header = "Nachname Präfix")]
        public string? NachnamePraefix { get; set; }

        public string Nachname { get; set; } = default!;

        [XLColumn(Header = "Suffix Titel")]
        public string? SuffixTitel { get; set; }

        public string? Geburtsname { get; set; }

        [XLColumn(Header = "Geburtsname Präfix")]
        public string? GeburtsnamePraefix { get; set; }

        public string? Geburtsort { get; set; }

        public DateTime? Geburtsdatum { get; set; }

        public string? Kolonie { get; set; }

        public int? Debitorennummer { get; set; }

        public string? Webseite { get; set; }
    }
}
