using BuisnessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TPIHM.Models;

namespace BuisnessLayer
{
    public class FilmDAO
    {
        

        public static List<FilmEntities> GetAllFilm()
        {

            List<FilmEntities> listFilm = new List<FilmEntities>();     
            return lectureFichier(listFilm);

        }

        public static List<FilmEntities> lectureFichier(List<FilmEntities> listFilm)
        {
            StreamReader streamReader = new StreamReader(@System.IO.Directory.GetParent(@Application.ResourceAssembly.Location).ToString() + "/films.txt");
            string ligne = streamReader.ReadLine();
            int nbfilm;
            List<PersonneEntities> acteurs = new List<PersonneEntities>();
            if (ligne == null) nbfilm = 0;
            else nbfilm = int.Parse(ligne);
            
            for (int i = 0; i < nbfilm; i++)
            {
                string s_nbacteurs = streamReader.ReadLine();
                int nbacteurs=0;
                int.TryParse(s_nbacteurs, out nbacteurs);
                string titre = streamReader.ReadLine();
                string titreFrancais = streamReader.ReadLine();
                string pays = streamReader.ReadLine();
                int budget;
                int.TryParse(streamReader.ReadLine(),out budget);
                PersonneEntities realisateur = new PersonneEntities(streamReader.ReadLine(), streamReader.ReadLine());
                int duree;
                int.TryParse(streamReader.ReadLine(),out duree);
                int annee;
                int.TryParse(streamReader.ReadLine(),out annee);
                int mois;
                int.TryParse(streamReader.ReadLine(),out mois);
                int jour;
                int.TryParse(streamReader.ReadLine(),out jour);
                Uri source = new Uri(@System.IO.Directory.GetParent(@Application.ResourceAssembly.Location).ToString() + streamReader.ReadLine());
                int note;
                int.TryParse(streamReader.ReadLine(), out note);
                string synopsis = streamReader.ReadLine();

                for (int j = 0; j < nbacteurs; j++)
                {
                    PersonneEntities p = new PersonneEntities(streamReader.ReadLine(), streamReader.ReadLine());

                    int anneeP;
                    int.TryParse(streamReader.ReadLine(), out anneeP);
                    int moisP;
                    int.TryParse(streamReader.ReadLine(), out moisP);
                    int jourP;
                    int.TryParse(streamReader.ReadLine(), out jourP);
                    p.DateNaissance = new Date(anneeP, moisP, jourP);
                    p.Photo= new Uri(@System.IO.Directory.GetParent(@Application.ResourceAssembly.Location).ToString() + streamReader.ReadLine());
                    acteurs.Add(p);
                }
                FilmEntities film = new FilmEntities()
                {
                    Titre = titre,
                    TitreFrancais = titreFrancais,
                    Pays = pays,
                    Budget = budget,
                    Duree = duree,
                    Realisateur = realisateur,
                    DateSortie = new Date(annee, mois, jour),
                    Source = source,
                    Note = note,
                    Synopsis = synopsis,
                    Acteurs=acteurs
                };
                listFilm.Add(film);
                acteurs = new List<PersonneEntities>();
            }
            streamReader.Close();
            return listFilm;
        }
    }
}
