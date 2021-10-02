using System.Collections.Generic;
using NodaTime;

namespace AV.AvA.Model;

public class Adresse
{
    public ZonedDateTime? GueltigVon { get; set; }

    public ZonedDateTime? GueltigBis { get; set; }

    public string Strasse { get; set; } = default!;

    public string PLZ { get; set; } = default!;

    public string Ort { get; set; } = default!;

    public string Land { get; set; } = default!;

    public string? Telefon { get; set; }

    public string? Fax { get; set; }

    public AdressTyp Typ { get; set; }

    public List<AdressFlag> Flags { get; set; } = default!;
}
