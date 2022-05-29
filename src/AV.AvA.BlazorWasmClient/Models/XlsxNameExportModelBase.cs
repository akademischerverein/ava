using ClosedXML.Attributes;

namespace AV.AvA.BlazorWasmClient.Models
{
    public class XlsxNameExportModelBase
    {
        [XLColumn(Header = "Präfix Titel", Order = 0)]
        public string? PraefixTitel { get; set; }

        [XLColumn(Order = 1)]
        public string Vorname { get; set; } = default!;

        [XLColumn(Order = 2)]
        public string? Zweitnamen { get; set; }

        [XLColumn(Header = "Nachname Präfix", Order = 3)]
        public string? NachnamePraefix { get; set; }

        [XLColumn(Order = 4)]
        public string Nachname { get; set; } = default!;

        [XLColumn(Header = "Suffix Titel", Order = 5)]
        public string? SuffixTitel { get; set; }
    }
}
