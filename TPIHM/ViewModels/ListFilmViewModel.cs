using BuisnessLayer;
using Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using TPIHM.Events;
using TPIHM.Factorys;
using View.Models;

namespace TPIHM.ViewModels
{
    public class ListFilmViewModel : NotifyPropertyChangedBase
    {
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand SupprCommand { get; set; }
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }



        private AddView _addView { get; set; }
        private EditView _editView { get; set; }
        private Validation _validView { get; set; }

        public String ToSearch { get; set; }

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
                CommandChangedEvent.GetEvent().OnButtonPressedActionHandler(EventArgs.Empty);
                NotifyPropertyChanged("Film");
                NotifyPropertyChanged("ListFilm");
                EditCommand.RaiseCanExecuteChanged();
                SupprCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<Film> _listFilm;
        public ObservableCollection<Film> ListFilm
        {
            get
            {
                return _listFilm;
            }
            set
            {
                _listFilm = value;
                _listFilm = new ObservableCollection<Film>(_listFilm.OrderByDescending(a => a.Titre));
            }
        }

        private ObservableCollection<Film> _toDisplay;
        public ObservableCollection<Film> ToDisplay
        {
            get
            {
                return _toDisplay;
            }
            set
            {
                _toDisplay = value;
                _toDisplay = new ObservableCollection<Film>(_toDisplay.OrderByDescending(a => a.Titre));
            }
        }

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

        /// <summary>
        /// 
        /// </summary>
        public ListFilmViewModel()
        {
            ListFilm = FilmFactory.AllFilmEntitieToFilm(FilmDAO.GetAllFilm());
            ToDisplay = ListFilm;

            AddCommand = new DelegateCommand(OnAddCommand, CanAddCommand);
            EditCommand = new DelegateCommand(OnEditCommand, CanEditCommand);
            SupprCommand = new DelegateCommand(OnSupprCommand, CanSupprCommand);
            SearchCommand = new DelegateCommand(OnSearchCommand, CanSearchCommand);
            SaveCommand = new DelegateCommand(OnSaveCommand, CanSaveCommand);
        }

        public void enregistrer(string fichier)
        {

            
            System.IO.File.Delete(@System.IO.Directory.GetParent(@Application.ResourceAssembly.Location).ToString() + fichier);
            StreamWriter streamWriter = new StreamWriter(@System.IO.Directory.GetParent(@Application.ResourceAssembly.Location).ToString() + fichier, true);

            int nbfilm = ListFilm.Count();
            streamWriter.WriteLine(nbfilm);
            foreach (Film f in ListFilm)
            {
                streamWriter.WriteLine(f.Acteurs.Count);
                streamWriter.WriteLine(f.Titre);
                streamWriter.WriteLine(f.TitreFrancais);
                streamWriter.WriteLine(f.Pays);
                streamWriter.WriteLine(f.Budget);
                streamWriter.WriteLine(f.Realisateur.Nom);
                streamWriter.WriteLine(f.Realisateur.Prenom);
                streamWriter.WriteLine(f.Duree);
                streamWriter.WriteLine(f.DateSortie.Annee);
                streamWriter.WriteLine(f.DateSortie.Mois);
                streamWriter.WriteLine(f.DateSortie.Jour);
                streamWriter.WriteLine("/" + Path.GetFileName(f.Source.LocalPath));
                streamWriter.WriteLine(f.Note);
                streamWriter.WriteLine(f.Synopsis);
                foreach (Personne a in f.Acteurs)
                {
                    streamWriter.WriteLine(a.Nom);
                    streamWriter.WriteLine(a.Prenom);
                    streamWriter.WriteLine(a.DateNaissance.Annee);
                    streamWriter.WriteLine(a.DateNaissance.Mois);
                    streamWriter.WriteLine(a.DateNaissance.Jour);
                    streamWriter.WriteLine("/" + Path.GetFileName(a.Photo.LocalPath));
                }
            }
            streamWriter.Close();
        }

        public ObservableCollection<Film> Search()
        {
            ObservableCollection<Film> results = new ObservableCollection<Film>();
            String titre;
            String titreFrancais;
            if (ToSearch != null) ToSearch = ToSearch.ToLower();
            foreach (Film film in ListFilm)
            {
                if (film.Titre != null) titre = film.Titre.ToLower();
                else titre = "";
                if (film.TitreFrancais != null) titreFrancais = film.TitreFrancais.ToLower();
                else titreFrancais = "";
                if (titre.Contains(ToSearch) || titreFrancais.Contains(ToSearch))
                {
                    results.Add(film);
                }
            }
            return results;
        }

        private ObservableCollection<Film> ListSort(ObservableCollection<Film> listFilm)
        {
            return new ObservableCollection<Film>(listFilm.OrderByDescending(a => a.Titre));
        }
        
        private void CloseAddWindows(object sender, EventArgs e)
        {

            _addView.Close();
            CommandChangedEvent.GetEvent().Handler -= CloseAddWindows;
        }

        private void CloseEditWindows(object sender, EventArgs e)
        {

            _editView.Close();
            CommandChangedEvent.GetEvent().Handler -= CloseEditWindows;
        }

        private void CloseValidWindows(object sender, EventArgs e)
        {

            _validView.Close();
            CommandChangedEvent.GetEvent().Handler -= CloseValidWindows;
        }

        private void OnAddCommand(object o)
        {

            CommandChangedEvent.GetEvent().Handler += CloseAddWindows;

            _addView = new AddView();
            _addView.Name = "Ajouter";
            _addView.ShowDialog();



            if (_addView.FilmViewModel.Valid == true)
            {
                ListFilm.Add(_addView.FilmViewModel.Film);
                ListFilm = ListSort(ListFilm);
                ToDisplay = ListFilm;
                NotifyPropertyChanged("ToDisplay");
                System.Windows.Forms.MessageBox.Show("Ajout effectué.");
            }
            else System.Windows.Forms.MessageBox.Show("Ajout annulé.");
        }



        private void OnEditCommand(object o)
        {
            CommandChangedEvent.GetEvent().Handler += CloseEditWindows;

            _editView = new EditView(Film);
            _editView.Name = "Editer";
            _editView.ShowDialog();


            if (_editView.FilmView.Valid == true)
            {
                ListFilm.Remove(Film);
                ListFilm.Add(_editView.FilmView.Film);
                ToDisplay = ListFilm;
                NotifyPropertyChanged("ToDisplay");
                System.Windows.Forms.MessageBox.Show("Edition effectuée.");
            }
            else System.Windows.Forms.MessageBox.Show("Edition annulée.");
        }

        private void OnSupprCommand(object o)
        {

            CommandChangedEvent.GetEvent().Handler += CloseValidWindows;
            _validView = new Validation("Voulez vous vraiment supprimer ce film ?");
            _validView.Name = "Validation";
            _validView.ShowDialog();
            if (_validView.ValidView.Valid == true)
            {
                ListFilm.Remove(Film);
                ToDisplay = ListFilm;
                NotifyPropertyChanged("ToDisplay");
                System.Windows.Forms.MessageBox.Show("Suppression effectuée.");
            }
            else System.Windows.Forms.MessageBox.Show("Suppression annulée.");
        }

        private void OnSearchCommand(object o)
        {
            ObservableCollection<Film> results = Search();
            if (results == null) return;
            ToDisplay = results;
            NotifyPropertyChanged("ToDisplay");
        }

        private void OnSaveCommand(object o)
        {
            CommandChangedEvent.GetEvent().Handler += CloseValidWindows;
            _validView = new Validation("Voulez vous vraiment enregistrer tous les changements ?");
            _validView.Name = "Validation";
            _validView.ShowDialog();
            if (_validView.ValidView.Valid == true)
            {
                enregistrer("/films.txt");
                System.Windows.Forms.MessageBox.Show("Enregistrement effectué.");
            }
            else System.Windows.Forms.MessageBox.Show("Enregistrement annulé.");
        }


        private bool CanAddCommand(object o)
        {
            return true;
        }

        private bool CanEditCommand(object o)
        {
            return Film != null;
        }

        private bool CanSupprCommand(object o)
        {
            return Film != null;
        }

        private bool CanSearchCommand(object o)
        {
            return true;
        }

        private bool CanSaveCommand(object o)
        {
            return true;
        }


    }
}

