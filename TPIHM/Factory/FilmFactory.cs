using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Models;
using BuisnessLayer.Entities;
using TPIHM.Models;

namespace TPIHM.Factorys
{
    public static class FilmFactory
    {
        public static Film FilmEntitiesToFilmModele(FilmEntities film)
        {
            List<Personne> tmp = new List<Personne>();
            foreach (PersonneEntities p in film.Acteurs)
            {
                Personne p1 = new Personne(p.Nom, p.Prenom);
                p1.DateNaissance = new Date(p.DateNaissance.Annee, p.DateNaissance.Mois, p.DateNaissance.Jour);
                p1.Photo = p.Photo;
                tmp.Add(p1);
            }
            return new Film
            {
                Titre = film.Titre,
                TitreFrancais = film.TitreFrancais,
                Pays = film.Pays,
                Budget = film.Budget,
                Source = film.Source,
                Note=film.Note,
                Realisateur = new Personne(film.Realisateur.Nom, film.Realisateur.Prenom),
                Duree = film.Duree,
                DateSortie = film.DateSortie,
                Synopsis= film.Synopsis,
                Acteurs=new ObservableCollection<Personne>(tmp)
            };
        }

        public static ObservableCollection<Film> AllFilmEntitieToFilm(List<FilmEntities> list)
        {
            return new ObservableCollection<Film>(list.Select(FilmEntitiesToFilmModele).ToList());
        }
    }
}
