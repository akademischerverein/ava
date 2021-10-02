using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;

namespace AV.AvA.Model
{
    public class Person
    {
        public Geschlecht Geschlecht;
        public string PraefixTitel;
        public string Vorname;
        public string Zweitnamen;
        public List<string> Spitznamen;
        public string NachnamePraefix;
        public string Nachname;
        public string SuffixTitel;
        public string Geburtsname;
        public string GeburtsnamePraefix;
        public string Geburtsort;
        public LocalDate Geburtsdatum;
        public string Kolonie;
        public int Debitorennummer;
        public string Webseite;

        public List<Adresse> Adressen;
        public List<Mobilnummer> Mobilnummern;
        public List<Email> Emails;
        public List<Arbeitgeber> Arbeitgeber;
        public List<Ausbildung> Ausbildungen;
        public List<Messenger> Messenger;
        public List<StatusEreignis> Status;
        public List<Beziehung> Beziehungen;
    }
}
