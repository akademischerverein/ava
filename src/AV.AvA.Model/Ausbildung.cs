using NodaTime;

namespace AV.AvA.Model;

public class Ausbildung
{
    public ZonedDateTime? GueltigVon { get; set; }

    public ZonedDateTime? GueltigBis { get; set; }

    public string Name { get; set; } = default!;

    public string Abschluss { get; set; } = default!;

    public bool AbschlussErreicht { get; set; }
}
