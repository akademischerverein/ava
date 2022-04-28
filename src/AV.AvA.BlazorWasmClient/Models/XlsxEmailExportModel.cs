using AV.AvA.Model;
using ClosedXML.Attributes;

namespace AV.AvA.BlazorWasmClient.Models
{
    public class XlsxEmailExportModel
    {
        [XLColumn(Header = "E-Mail Adresse", Order = 0)]
        public string EmailAddress { get; set; }

        [XLColumn(Header = "E-Mail Typ", Order = 1)]
        public EmailTyp EmailTyp { get; set; }

        [XLColumn(Header = "Präfix Titel", Order = 2)]
        public string? PraefixTitel { get; set; }

        [XLColumn(Order = 3)]
        public string Vorname { get; set; } = default!;

        [XLColumn(Order = 4)]
        public string? Zweitnamen { get; set; }

        [XLColumn(Header = "Nachname Präfix", Order = 5)]
        public string? NachnamePraefix { get; set; }

        [XLColumn(Order = 6)]
        public string Nachname { get; set; } = default!;

        [XLColumn(Header = "Suffix Titel", Order = 7)]
        public string? SuffixTitel { get; set; }
    }
}
