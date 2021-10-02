using NodaTime;

namespace AV.AvA.Model;

public class Ausbildung
{
    public ZonedDateTime? GueltigVon;
    public ZonedDateTime? GueltigBis;

    public string Name;
    public string Abschluss;
    public bool AbschlussErreicht;
}
