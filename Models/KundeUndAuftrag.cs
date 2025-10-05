using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Models
{
    public class KundeUndAuftrag
    {

        public Kunde Kunde { get; set; }
        public List<Auftrag> AufträgeVomKundenListe { get; set; } = new List<Auftrag>();

    }
}
