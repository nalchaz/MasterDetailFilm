using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using View.Models;
using TPIHM.Events;
using static TPIHM.Events.CommandChangedEvent;
using Microsoft.Win32;
using System.Windows;
using TPIHM.Models;
using System.Collections.ObjectModel;

namespace TPIHM.ViewModels
{
    public class FilmViewModel : NotifyPropertyChangedBase
    {
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand BrowseCommand { get; set; }
        public DelegateCommand AddActCommand { get; set; }
        public DelegateCommand SupprActCommand { get; set; }

        public Uri Parcourir { get; set; }

        public bool Valid { get; set; }

        private AddActView _addActView { get; set; }

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

        private Film _film;
        public Film Film
        {
            get
            {
                return _film;
            }
            set
            {
                _film = value;
                NotifyPropertyChanged("Film");
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public FilmViewModel(Film film)
        {
            AddCommand = new DelegateCommand(OnAddCommand, CanAddCommand);
            CancelCommand = new DelegateCommand(OnCancelCommand, CanCancelCommand);
            BrowseCommand = new DelegateCommand(OnBrowseCommand, CanBrowseCommand);
            AddActCommand = new DelegateCommand(OnAddActCommand, CanAddActCommand);
            SupprActCommand = new DelegateCommand(OnSupprActCommand, CanSupprActCommand);

            Film = film;
            if (Film.Realisateur == null)
            {
                Film.Realisateur = new Personne("Nom", "Prenom");
            }
            if (Film.DateSortie == null)
            {
                Film.DateSortie = new Date(1, 1, 1);
            }
            if (Film.Source == null) Parcourir = new Uri(System.IO.Directory.GetParent(Application.ResourceAssembly.Location).ToString() + "/photoVide.jpg");
            else Parcourir = Film.Source;

            if (Film.Acteurs == null)
            {
                Film.Acteurs = new ObservableCollection<Personne>();
            }

        }

        public void FileCopy(string source, string fileName)
        {

            if (System.IO.Directory.GetParent(source).ToString() != System.IO.Directory.GetParent(Application.ResourceAssembly.Location).ToString())
            {
                string targetFile = System.IO.Directory.GetParent(Application.ResourceAssembly.Location).ToString();
                System.IO.File.Copy(@source, @targetFile + "/" + fileName, true);
            }
            
        }
    

        private void OnAddCommand(object o)
        {
            string source = Parcourir.ToString();
            string fileName = System.IO.Path.GetFileName(Parcourir.ToString());
            FileCopy(Parcourir.LocalPath, fileName);
            
            Film.Source = new Uri(@System.IO.Directory.GetParent(Application.ResourceAssembly.Location).ToString() + "/" + fileName);
            Valid = true;
            CommandChangedEvent.GetEvent().OnButtonPressedActionHandler(EventArgs.Empty);
        }

        private void OnCancelCommand(object o)
        {
            Valid = false;
            CommandChangedEvent.GetEvent().OnButtonPressedActionHandler(EventArgs.Empty);
        }

        private void OnBrowseCommand(object o)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Image Files (*.bmp, *.jpg)|*.bmp;*.jpg";
            f.ShowDialog();
            String chemin = f.FileName;
            if(chemin != "")
            Parcourir = new Uri(@chemin);
            NotifyPropertyChanged("Parcourir");
        }

        private void CloseAddActWindows(object sender, EventArgs e)
        {

            _addActView.Close();
            CommandChangedEvent2.GetEvent().Handler -= CloseAddActWindows;
        }

        private void OnAddActCommand(object o)
        {

            CommandChangedEvent2.GetEvent().Handler += CloseAddActWindows;

            _addActView = new AddActView();
            _addActView.Name = "Ajouter";
            _addActView.ShowDialog();

            if (_addActView.ActView.Valid == true)
            {
                Film.Acteurs.Add(_addActView.ActView.Acteur);
                NotifyPropertyChanged("Film");
            }

        }
        private void OnSupprActCommand(object o)
        {
              Film.Acteurs.Remove(Acteur);
              NotifyPropertyChanged("Film");

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

        private bool CanAddActCommand(object o)
        {
            return true;
        }

        private bool CanSupprActCommand(object o)
        {
            return true;
        }
    }
}