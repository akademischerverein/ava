using System.ComponentModel.DataAnnotations;

namespace AV.AvA.Model;

public enum StatusEreignisTyp
{
    [Display(Name = "Studentischer Gast: eingezogen")]
    StudentischerGastEingezogen = 1,

    [Display(Name = "Studentischer Gast: ausgezogen")]
    StudentischerGastAusgezogen,

    [Display(Name = "Jungmitglied: eingesungen")]
    JungmitgliedEingesungen,

    [Display(Name = "Verkehrsgast: aufgenommen")]
    VerkehrsgastAufgenommen,

    [Display(Name = "Vollmitglied: aufgenommen")]
    VollmitgliedAufgenommen,

    [Display(Name = "Inaktivierung")]
    Inaktivierung,

    [Display(Name = "Beurlaubt")]
    Beurlaubt,

    [Display(Name = "Übertritt in ADAHschaft")]
    Uebertritt,

    [Display(Name = "Ex-Aktiv")]
    ExAktivierung,

    [Display(Name = "Stud. Mitglied HG: Eintritt")]
    EintrittStudentischesMitgliedHG,

    [Display(Name = "Stud. Mitglied AV e.V.: Eintritt")]
    EintrittStudentischesMitgliedAVeV,

    [Display(Name = "Ord. Mitglied HG: Eintritt")]
    EintrittOrdentlichesMitgliedHG,

    [Display(Name = "Ord. Mitglied AV e.V.: Eintritt")]
    EintrittOrdentlichesMitgliedAVeV,

    [Display(Name = "Förd. Mitglied HG: Eintritt")]
    EintrittFoerderndesMitgliedHG,

    [Display(Name = "Förd. Mitglied AV e.V.: Eintritt")]
    EintrittFoerderndesMitgliedAVeV,

    [Display(Name = "Stimmrecht HG erhalten")]
    StimmrechtHGErhalten,

    [Display(Name = "GAV: Eintritt")]
    EintrittGAV,

    [Display(Name = "Ehrenmitgliedschaft erhalten")]
    EhrenmitgliedschaftErhalten,

    [Display(Name = "Verwittwert")]
    Verwittwert,

    [Display(Name = "Verstorben")]
    Verstorben,

    [Display(Name = "Aktivitas: ausgeschieden")]
    AusscheidenAktivitas,

    [Display(Name = "AV e.V.: ausgeschieden")]
    AusscheidenAVeV,

    [Display(Name = "HG: ausgeschieden")]
    AusscheidenHG,

    [Display(Name = "GAV: ausgeschieden")]
    AusscheidenGAV,
}
