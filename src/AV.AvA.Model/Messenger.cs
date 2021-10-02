using NodaTime;

namespace AV.AvA.Model;

public class Messenger
{
    public ZonedDateTime? GueltigVon { get; set; }

    public ZonedDateTime? GueltigBis { get; set; }

    public string Name { get; set; } = default!;

    public MessengerTyp Typ { get; set; }
}
