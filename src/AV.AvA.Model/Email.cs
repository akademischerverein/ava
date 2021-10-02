using System.Collections.Generic;
using NodaTime;

namespace AV.AvA.Model;

public class Email
{
    public ZonedDateTime? GueltigVon { get; set; }

    public ZonedDateTime? GueltigBis { get; set; }

    public string Adresse { get; set; } = default!;

    public AdressTyp Typ { get; set; }

    public List<EmailFlag> Flags { get; set; } = default!;
}
