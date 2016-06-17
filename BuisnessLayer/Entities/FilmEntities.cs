using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TPIHM.Models;

namespace BuisnessLayer.Entities
{
    public class FilmEntities
    {
        public String Titre { get; set; }
        public String TitreFrancais { get; set; }
        public String Pays { get; set; }
        public int Budget { get; set; }
        public PersonneEntities Realisateur { get; set; }
        public int Duree { get; set; }
        public Date DateSortie { get; set; }
        public Uri Source { get; set; }
        public int Note { get; set; }
        public String Synopsis { get; set; }
        public List<PersonneEntities> Acteurs { get; set; }
    }
}
