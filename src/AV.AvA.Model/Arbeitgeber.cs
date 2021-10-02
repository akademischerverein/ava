using NodaTime;

namespace AV.AvA.Model;

public class Arbeitgeber
{
    public ZonedDateTime? GueltigVon { get; set; }

    public ZonedDateTime? GueltigBis { get; set; }

    public string Name { get; set; } = default!;

    public string? Abteilung { get; set; }

    public string? Position { get; set; }

    public Adresse Adresse { get; set; } = default!;
}
