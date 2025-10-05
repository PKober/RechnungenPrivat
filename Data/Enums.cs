using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Data
{
    public class Enums
    {

        public enum Auftragstyp
        {
            Pauschal,
            Stundenbasiert
        }


        public enum EnumEinnahmentyp
        {
            Privat,
            Gewerblich,
            Arbeitgeber
        }


        public enum EnumAusgabeTyp
        {
            Privat,
            Gewerblich
        }
    }
}
