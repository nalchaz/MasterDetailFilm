using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPIHM.Models;

namespace View.Models
{
    public class Personne : NotifyPropertyChangedBase
    {
        private String _nom;
        public string Nom
        {
            get
            {
                return _nom;
            }

            set
            {
                _nom = value;
            }
        }

        private String _prenom;
        public string Prenom
        {
            get
            {
                return _prenom;
            }

            set
            {
                _prenom = value;
            }
        }

        private Date _dateNaissance;
        public Date DateNaissance
        {
            get
            {
                return _dateNaissance;
            }
            set
            {
                _dateNaissance = value;
            }
        }

        public Uri Photo { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Prenom, Nom);
        }

        public Personne(string nom,string prenom)
        {
            Nom = nom;
            Prenom = prenom;
        }
    }
}
