using System.Collections.Generic;
using NodaTime;

namespace AV.AvA.Model;

public class Email
{
    public ZonedDateTime GueltigVon;
    public ZonedDateTime GueltigBis;

    public string Adresse;
    public AdressTyp Typ;
    public List<AdressFlag> Flags;
}
