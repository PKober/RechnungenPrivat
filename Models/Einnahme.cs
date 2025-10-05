using SQLite;
using System;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Models
{
    [Table("Einnahme")]
    public class Einnahme
    {
        [PrimaryKey, AutoIncrement]
        public int EinnahmenId { get; set; }
        [ForeignKey(typeof(Auftrag))]
        public int Auftrags_ID { get; set; }
    }
}
