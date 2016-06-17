using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.Windows.Media;
using TPIHM.Models;
using System.Collections.ObjectModel;

namespace View.Models
{
    public class Film : NotifyPropertyChangedBase
    {
        public String Titre { get; set; }
        public String TitreFrancais { get; set; }
        public String Pays { get; set; }
        public int Budget { get; set; }
        public Personne Realisateur { get; set; }
        public int Duree { get; set; }
        public Date DateSortie { get; set; }
        public Uri Source { get; set; }
        public int Note { get; set; }
        public String Synopsis { get; set; }
        public ObservableCollection<Personne> Acteurs { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Titre, TitreFrancais);
        }
        
    }
}
