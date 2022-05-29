using ClosedXML.Attributes;
using NodaTime;

namespace AV.AvA.BlazorWasmClient.Models
{
    public class XlsxBirthdayListExportModel : XlsxNameExportModelBase
    {
        [XLColumn(Header = "Geburtsdatum", Order = 6)]
        public DateTime? Geburtsdatum { get; set; }
    }
}
