using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IHFF_Websystem.Models
{
    public class WishlistRepository
    {
        public IHFFContext ctx = new IHFFContext();

        public void AddDiner(int wishlistID, Diner diner)
        {
            diner.wishlistID = wishlistID;
            ctx.Diners.Add(diner);
            ctx.SaveChanges();
        }
        public void AddEvenement(int wishlistID, int evenementID)
        {
            WishlistEvenement wishlistevent = new WishlistEvenement(wishlistID, evenementID);
            ctx.WishlistEvenements.Add(wishlistevent);
            ctx.SaveChanges();
        }
        public Evenement GetEvent(int ID)
        {
            return ctx.Evenementen.SingleOrDefault(c => c.evenementID == ID);   
        }
        public Locatie GetLocatie(int ID)
        {
            return ctx.Locaties.SingleOrDefault(c => c.locatieID == ID);
        }
        public Film GetFilm(int ID)
        {
            return ctx.Films.SingleOrDefault(c => c.evenementID == ID);
        }
        public IEnumerable<Film> GetAllFilms()
        {
            IEnumerable<Film> films = ctx.Films;
            return films;
        }
        public IEnumerable<Film> GetAllWishlistFilms(int wishlistID)
        {
            List<Film> films = new List<Film>();
            IEnumerable<WishlistEvenement> wishlistevents = ctx.WishlistEvenements.Where(w => w.wishlistID == wishlistID);
            foreach (WishlistEvenement wishlistevent in wishlistevents) {
                //quick fix
                films.Add(new WishlistRepository().ctx.Films.SingleOrDefault(f => f.evenementID == wishlistevent.evenementID));
            }
            return films;
        }
        public void CreateFilm(string evenementNaam, DateTime startTijd, string beschrijving, double prijs, string regisseur, int locatieID)
        {
            int evenementID = 1;
            Film Myevent = new Film(evenementID, evenementNaam, startTijd, beschrijving, prijs, regisseur, locatieID);
            ctx.Films.Add(Myevent);
            ctx.SaveChanges();
        }
    }
}
