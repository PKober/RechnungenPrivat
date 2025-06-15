using System;
using System.Collections.Generic;
using SQLite;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Models
{
    [Table("Ausgabe")]
    public class Ausgabe
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }

        [MaxLength(200)]
        public string Bezeichnung { get; set; }

        public decimal Betrag { get; set; }
        public DateTime Datum { get; set; }
        public byte[] BelegFoto { get; set; }
        public string Notizen { get; set; }
    }
}
