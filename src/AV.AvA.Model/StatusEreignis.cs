using System.Collections.Generic;
using NodaTime;

namespace AV.AvA.Model;

public class StatusEreignis
{
    public ZonedDateTime EingetretenAm;
    public ZonedDateTime? BefristedBis;

    public string? Grund;
    public StatusEreignisTyp Typ;

    public List<Status> ErmittelterStatus;
}
