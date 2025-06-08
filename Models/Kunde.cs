using SQLite;

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
