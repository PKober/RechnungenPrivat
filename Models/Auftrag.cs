using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Models
{

    public enum Auftragstyp
    {
        Pauschal,
        Stundenbasiert
    }

    [Table("Auftrag")]
    public class Auftrag 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int KundeId { get; set; }
        [MaxLength(500)]
        public string Beschreibung { get; set;}

        public decimal Betrag { get; set; }
        public DateOnly Auftragsdatum;
        public Auftragstyp Typ { get; set; }


        public decimal? Stunden { get; set; }
        public decimal? Stundensatz { get; set; }
        public string Auftragsname { get; set; }

    }
}
    