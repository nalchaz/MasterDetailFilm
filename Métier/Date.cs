using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIHM.Models
{
    public class Date
    {
        public int Jour { get; set; }
        public int Mois { get; set; }
        public int Annee { get; set; }

        public Date(int annee, int mois, int jour)
        {
            Annee = annee;
            Mois = mois;
            Jour = jour;
        }

        override
        public String ToString()
        {
            return Jour + "/" + Mois + "/" + Annee;
        }
    }
}
