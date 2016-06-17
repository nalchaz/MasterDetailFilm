using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPIHM.Models;

namespace BuisnessLayer.Entities
{
    public class PersonneEntities
    {
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public Date DateNaissance { get; set; }
        public Uri Photo { get; set; }

        public PersonneEntities(string nom, string prenom)
        {
            Nom = nom;
            Prenom = prenom;
        }
    }
}
