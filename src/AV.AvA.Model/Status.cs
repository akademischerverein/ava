using NodaTime;

namespace AV.AvA.Model;

public class Status
{
    public ZonedDateTime GueltigVon;
    public ZonedDateTime GueltigBis;

    public string? Grund;
    public StatusTyp Typ;
}
