using AV.AvA.Model;
using ClosedXML.Attributes;

namespace AV.AvA.BlazorWasmClient.Models
{
    public class XlsxEmailExportModel : XlsxNameExportModelBase
    {
        [XLColumn(Header = "E-Mail Adresse", Order = 6)]
        public string EmailAddress { get; set; } = null!;

        [XLColumn(Header = "E-Mail Typ", Order = 7)]
        public EmailTyp EmailTyp { get; set; }
    }
}
