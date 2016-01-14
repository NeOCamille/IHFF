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
        public void AddEvenement(int wishlistID, int evenementID, uint aantal)
        {
            WishlistEvenement wishlistevent = new WishlistEvenement(wishlistID, evenementID, aantal);
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
          
            //ctx.Films.Include()
            return films;
        }

        public IEnumerable<Special> GetAllWishlistSpecials(int wishlistID)
        {
            List<Special> specials = new List<Special>();
            IEnumerable<WishlistEvenement> wishlistevents = ctx.WishlistEvenements.Where(w => w.wishlistID == wishlistID);
            foreach (WishlistEvenement wishlistevent in wishlistevents)
            {
                //quick fix
                specials.Add(new WishlistRepository().ctx.Specials.SingleOrDefault(f => f.evenementID == wishlistevent.evenementID));
            }

            //ctx.Films.Include()
            return specials;
        }
        public IEnumerable<Diner> GetAllWishlistDiners(int wishlistID)
        {
            IEnumerable<Diner> diners = ctx.Diners.Where(w => w.wishlistID == wishlistID);

            //ctx.Films.Include()
            return diners;
        }


        public IEnumerable<WishlistEvenement> GetAllWishlistEvenements(int wishlistID)
        {
            IEnumerable<WishlistEvenement>wishlistEvenements = ctx.WishlistEvenements.Where(w => w.wishlistID == wishlistID);           
            return wishlistEvenements;
        }
        public void CreateFilm(string evenementNaam, DateTime startTijd, string beschrijving, double prijs, string regisseur, int locatieID)
        {
            int evenementID = 1;
            Film Myevent = new Film(evenementID, evenementNaam, startTijd, beschrijving, prijs, regisseur, locatieID);
            ctx.Films.Add(Myevent);
            ctx.SaveChanges();
        }

        public List<Evenement> GetMyWishlistEvenements(int wishlistID)
        {
            List<Evenement> mywishlistevenement = new List<Evenement>();
            IEnumerable<WishlistEvenement> wishlistevents = ctx.WishlistEvenements.Where(w => w.wishlistID == wishlistID);
            foreach (WishlistEvenement wishlistevent in wishlistevents)
            {
                mywishlistevenement.Add(ctx.Evenementen.SingleOrDefault(e => e.evenementID == wishlistevent.evenementID));               
            }
            return mywishlistevenement;
        }
        
        public List<Diner> Getmywishlistdiner(int wishlistID)
        {
            List<Diner> mywishlistdiner = new List<Diner>();
            IEnumerable<Diner> diners = ctx.Diners.Where(d => d.wishlistID == wishlistID);
            foreach (Diner diner in diners)
            {
                mywishlistdiner.Add(diner);
            }
            return mywishlistdiner;

        }

        public WishlistEvenement GetWishlistEvenement(int wishlistID, int evenementID)
        {
            WishlistEvenement wishlistevenement = ctx.WishlistEvenements.SingleOrDefault(w => w.wishlistID == wishlistID && w.evenementID == evenementID);
            return wishlistevenement;
        }

        public void DeleteWishlistEvenement(int id)
        {
            WishlistEvenement wishlistEvenement = ctx.WishlistEvenements.Find(id);
            ctx.WishlistEvenements.Remove(wishlistEvenement);
            ctx.SaveChanges();
        }

        public void DeleteDiner(int id)
        {
            Diner diner = ctx.Diners.Find(id);
            ctx.Diners.Remove(diner);
            ctx.SaveChanges();
        }

        public void WishListReserveren(Wishlist wishlist)
        {
            wishlist.isBetaald = true;
            ctx.Wishlists.Find(wishlist.wishlistID).isBetaald = true;
            ctx.SaveChanges();
        }

        public Wishlist GetWishList(string codewoord)
        {
            return ctx.Wishlists.SingleOrDefault(w => w.codeWoord == codewoord);
        }

        public Wishlist GetWishList(int wishlistID)
        {
            return ctx.Wishlists.SingleOrDefault(w => w.wishlistID == wishlistID);
        }
    }
}
