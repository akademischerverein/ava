using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;

namespace AV.AvA.Model;

public class Person
{
    public Geschlecht Geschlecht { get; set; }

    public string? PraefixTitel { get; set; }

    public string Vorname { get; set; } = default!;

    public string? Zweitnamen { get; set; }

    public List<string> Spitznamen { get; set; } = new(0);

    public string? NachnamePraefix { get; set; }

    public string Nachname { get; set; } = default!;

    public string? SuffixTitel { get; set; }

    public string? Geburtsname { get; set; }

    public string? GeburtsnamePraefix { get; set; }

    public string? Geburtsort { get; set; }

    public LocalDate? Geburtsdatum { get; set; }

    public string? Kolonie { get; set; }

    public int? Debitorennummer { get; set; }

    public string? Webseite { get; set; }

    public List<Adresse> Adressen { get; set; } = new(0);

    public List<Mobilnummer> Mobilnummern { get; set; } = new(0);

    public List<Email> Emails { get; set; } = new(0);

    public List<Arbeitgeber> Arbeitgeber { get; set; } = new(0);

    public List<Ausbildung> Ausbildungen { get; set; } = new(0);

    public List<Messenger> Messenger { get; set; } = new(0);

    public List<StatusEreignis> Status { get; set; } = new(0);

    public List<Beziehung> Beziehungen { get; set; } = new(0);

    public bool AvASchreibzugriff { get; set; }

    public List<string> VerursachendeZeilen { get; set; } = new List<string>();

    public Dictionary<string, string> AltesAvaModel { get; set; } = new Dictionary<string, string>();
}
