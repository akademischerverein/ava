using System.Collections.Generic;
using NodaTime;

namespace AV.AvA.Model;

public class Adresse
{
    public ZonedDateTime? GueltigVon;
    public ZonedDateTime? GueltigBis;

    public string Strasse;
    public string PLZ;
    public string Ort;
    public string Land;
    public string? Telefon;
    public string? Fax;
    public AdressTyp Typ;
    public List<AdressFlag> Flags;
}
