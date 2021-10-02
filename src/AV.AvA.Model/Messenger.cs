using NodaTime;

namespace AV.AvA.Model;

public class Messenger
{
    public ZonedDateTime GueltigVon;
    public ZonedDateTime GueltigBis;

    public string Name;
    public MessengerTyp Typ;

}
