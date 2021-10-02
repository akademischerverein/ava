using NodaTime;

namespace AV.AvA.Model;

public class Beziehung
{
    public ZonedDateTime? GueltigVon;
    public ZonedDateTime? GueltigBis;

    public string? Vorname;
    public string? Nachname;
    public LocalDate? VerstorbenAm;
    public int? AvId;
    public BeziehungsTyp Typ;
}
