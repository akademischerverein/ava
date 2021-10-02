using NodaTime;

namespace AV.AvA.Model;

public class Ausbildung
{
    public ZonedDateTime GueltigVon;
    public ZonedDateTime GueltigBis;

    public string Ausbildungsbereich;
    public string Abschluss;
}
