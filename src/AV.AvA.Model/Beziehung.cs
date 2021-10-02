using NodaTime;

namespace AV.AvA.Model;

public class Beziehung
{
    public ZonedDateTime? GueltigVon { get; set; }

    public ZonedDateTime? GueltigBis { get; set; }

    public string? Vorname { get; set; }

    public string? Nachname { get; set; }

    public LocalDate? VerstorbenAm { get; set; }

    public int? AvId { get; set; }

    public BeziehungsTyp Typ { get; set; }
}
