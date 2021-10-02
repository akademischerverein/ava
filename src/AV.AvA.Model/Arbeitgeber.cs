using NodaTime;

namespace AV.AvA.Model;

public class Arbeitgeber
{
    public ZonedDateTime GueltigVon;
    public ZonedDateTime GueltigBis;

    public string Name;
    public string? Abteilung;
    public string? Position;
    public Adresse Adresse;
}
