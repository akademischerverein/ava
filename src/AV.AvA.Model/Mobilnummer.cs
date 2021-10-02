using NodaTime;

namespace AV.AvA.Model;

public class Mobilnummer
{
    public ZonedDateTime? GueltigVon;
    public ZonedDateTime? GueltigBis;

    public string Nummer;
    public AdressTyp Typ;
}
