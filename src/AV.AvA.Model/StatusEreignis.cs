using System.Collections.Generic;
using NodaTime;

namespace AV.AvA.Model;

public class StatusEreignis
{
    public ZonedDateTime EingetretenAm { get; set; }

    public ZonedDateTime? BefristedBis { get; set; }

    public string? Grund { get; set; }

    public StatusEreignisTyp Typ { get; set; }

    public List<Status> ErmittelterStatus { get; set; } = default!;
}
