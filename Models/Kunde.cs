using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Models
{
    [Table("Kunde")]
    public class Kunde
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(100)]
        public string KundenName { get; set; }
        [MaxLength(100)]
        public string KundenAdresse { get; set; }


        

    }
}
