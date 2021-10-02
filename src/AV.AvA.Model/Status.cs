using NodaTime;

namespace AV.AvA.Model;

public class Status
{
    public ZonedDateTime? GueltigVon { get; set; }

    public ZonedDateTime? GueltigBis { get; set; }

    public string? LetzterEreignisGrund { get; set; }

    public StatusTyp Typ { get; set; }
}
