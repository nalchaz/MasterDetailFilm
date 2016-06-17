using Library;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TPIHM.Events;
using TPIHM.Models;
using View.Models;

namespace TPIHM.ViewModels
{
    public class ActViewModel : NotifyPropertyChangedBase
    {

        public Uri Parcourir { get; set; }

        public bool Valid { get; set; }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand BrowseCommand { get; set; }

        private Personne _acteur;
        public Personne Acteur
        {
            get
            {
                return _acteur;
            }
            set
            {
                _acteur = value;
                NotifyPropertyChanged("Acteur");
            }
        }

        public ActViewModel()
        {
            Acteur = new Personne("", "");
            Acteur.DateNaissance = new Date(1, 1, 1);

            AddCommand = new DelegateCommand(OnAddCommand, CanAddCommand);
            CancelCommand = new DelegateCommand(OnCancelCommand, CanCancelCommand);
            BrowseCommand = new DelegateCommand(OnBrowseCommand, CanBrowseCommand);
            Parcourir = new Uri(System.IO.Directory.GetParent(Application.ResourceAssembly.Location).ToString() + "/photoVide.jpg");
        }





        private void OnAddCommand(object o)
        {
            string source = Parcourir.ToString();
            string fileName = System.IO.Path.GetFileName(Parcourir.ToString());
            if (System.IO.Directory.GetParent(Parcourir.LocalPath).ToString() != System.IO.Directory.GetParent(Application.ResourceAssembly.Location).ToString())
            {
                string targetFile = System.IO.Directory.GetParent(Application.ResourceAssembly.Location).ToString();
                System.IO.File.Copy(@Parcourir.LocalPath, @targetFile + "/" + fileName, true);
            }

            Acteur.Photo = new Uri(@System.IO.Directory.GetParent(Application.ResourceAssembly.Location).ToString() + "/" + fileName);
            Valid = true;
            CommandChangedEvent2.GetEvent().OnButtonPressedActionHandler2(EventArgs.Empty);
        }

        private void OnCancelCommand(object o)
        {
            Valid = false;
            CommandChangedEvent2.GetEvent().OnButtonPressedActionHandler2(EventArgs.Empty);
        }

        private void OnBrowseCommand(object o)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Image Files (*.bmp, *.jpg)|*.bmp;*.jpg";
            f.ShowDialog();
            String chemin = f.FileName;
            if (chemin != "")
                Parcourir = new Uri(@chemin);
            NotifyPropertyChanged("Parcourir");
        }


        private bool CanAddCommand(object o)
        {
            return true;
        }

        private bool CanCancelCommand(object o)
        {
            return true;
        }

        private bool CanBrowseCommand(object o)
        {
            return true;
        }
    }
}
