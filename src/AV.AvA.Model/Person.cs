using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;

namespace AV.AvA.Model;

public class Person
{
    public Geschlecht Geschlecht { get; set; }

    public string PraefixTitel { get; set; } = default!;

    public string Vorname { get; set; } = default!;

    public string Zweitnamen { get; set; } = default!;

    public List<string> Spitznamen { get; set; } = default!;

    public string NachnamePraefix { get; set; } = default!;

    public string Nachname { get; set; } = default!;

    public string SuffixTitel { get; set; } = default!;

    public string Geburtsname { get; set; } = default!;

    public string GeburtsnamePraefix { get; set; } = default!;

    public string Geburtsort { get; set; } = default!;

    public LocalDate Geburtsdatum { get; set; }

    public string Kolonie { get; set; } = default!;

    public int Debitorennummer { get; set; }

    public string Webseite { get; set; } = default!;

    public List<Adresse> Adressen { get; set; } = default!;

    public List<Mobilnummer> Mobilnummern { get; set; } = default!;

    public List<Email> Emails { get; set; } = default!;

    public List<Arbeitgeber> Arbeitgeber { get; set; } = default!;

    public List<Ausbildung> Ausbildungen { get; set; } = default!;

    public List<Messenger> Messenger { get; set; } = default!;

    public List<StatusEreignis> Status { get; set; } = default!;

    public List<Beziehung> Beziehungen { get; set; } = default!;
}
