using NodaTime;

namespace AV.AvA.Model;

public class Mobilnummer
{
    public ZonedDateTime? GueltigVon { get; set; }

    public ZonedDateTime? GueltigBis { get; set; }

    public string Nummer { get; set; } = default!;

    public AdressTyp Typ { get; set; }
}
