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

        //Create a new wishlist with ID
        public Wishlist NewWishlist()
        {
            Wishlist newWishlist = new Wishlist(0, "", false, false, "", 0.00);
            ctx.Wishlists.Add(newWishlist);
            ctx.SaveChanges();
            return newWishlist;
        }

        public void AddDiner(Diner diner)
        {
            ctx.Diners.Add(diner);
            ctx.SaveChanges();
        }
        public void AddEvenement(int wishlistID, int evenementID, int aantal)
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

        public IEnumerable<Tuple<Film, int>> GetAllWishlistFilms(int wishlistID)
        {
            //Using a tuple to get film and aantal
            List<Tuple<Film, int>> lijst = new List<Tuple<Film, int>>();
            IEnumerable<WishlistEvenement> wishlistevents = ctx.WishlistEvenements.Where(w => w.wishlistID == wishlistID);
            foreach (WishlistEvenement wishlistevent in wishlistevents) {
                //quick fix
                Tuple<Film, int> lijstItem =
                    new Tuple<Film, int>(
                        new WishlistRepository().ctx.Films.SingleOrDefault(f => f.evenementID == wishlistevent.evenementID),
                        wishlistevent.aantal);
                lijst.Add(lijstItem);
            }
          
            return lijst;
        }
        public IEnumerable<Tuple<Special, int>> GetAllWishlistSpecials(int wishlistID)
        {
            List<Tuple<Special, int>> lijst = new List<Tuple<Special, int>>();
            IEnumerable<WishlistEvenement> wishlistevents = ctx.WishlistEvenements.Where(w => w.wishlistID == wishlistID);
            foreach (WishlistEvenement wishlistevent in wishlistevents)
            {
                Tuple<Special, int> lijstItem =
                    new Tuple<Special, int>(
                        new WishlistRepository().ctx.Specials.SingleOrDefault(f => f.evenementID == wishlistevent.evenementID),
                        wishlistevent.aantal);
                lijst.Add(lijstItem);
            }

            //ctx.Films.Include()
            return lijst;
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

        //Check available seats
        public int CheckAvailabilityEvenement(int myEvenementID)
        {
            //SELECT COUNT (WishlistEvenements.aantal)
            //FROM Evenements
            //INNER JOIN WishlistEvenements
            //ON Evenements.evenementID = WishlistEvenements.evenementID
            //INNER JOIN Wishlists
            //ON Wishlists.wishlistID = WishlistEvenements.wishlistID
            //WHERE Wishlists.isBetaald = '1'

            // SQL statement to get all aantals
            var eventAantal = (from e in ctx.Evenementen
                                join we in ctx.WishlistEvenements on e.evenementID equals we.evenementID
                                join w in ctx.Wishlists on we.wishlistID equals w.wishlistID
                                where w.isBetaald == true
                                where e.evenementID == myEvenementID
                                select new
                                {
                                    aantal = we.aantal
                                });

            // add all aantals
            int total = 0;
            foreach (var single in eventAantal)
            {
                total += single.aantal;
            }


            //get maxAantal plaatsen for evenementID
            var eventMaxAantal = (from e in ctx.Evenementen
                               join l in ctx.Locaties on e.locatieID equals l.locatieID
                                  where e.evenementID == myEvenementID
                               select new
                               {
                                   maxAantalPlaatsen = l.maxAantalPlaatsen
                               }).Single(); //single gives exeption if more then one
            

            // return available seats
            return (eventMaxAantal.maxAantalPlaatsen - total);
        }

        //update aantal for a wishlistEvenement
        public void UpdateAantal_WE(int evenementID, int wishlistID, int aantal)
        {
            try
            {
                if (aantal <= 0)
                {
                    var test = ctx.WishlistEvenements.Where(w => w.wishlistID == wishlistID).Where(w => w.evenementID == evenementID);
                    //Only change one
                    WishlistEvenement updated = test.First();
                    ctx.WishlistEvenements.Remove(updated);
                    ctx.SaveChanges();
                }
                else
                {

                    var test = ctx.WishlistEvenements.Where(w => w.wishlistID == wishlistID).Where(w => w.evenementID == evenementID);
                    //Only change one
                    WishlistEvenement updated = test.First();
                    updated.aantal = aantal;

                    ctx.WishlistEvenements.Attach(updated);
                    var entry = ctx.Entry(updated);
                    entry.Property(e => e.aantal).IsModified = true;
                    ctx.SaveChanges();
                }
            } catch(Exception ex) {  }
        }

        //Update aantal for Diner
        public void UpdateAantal_D(int dinerID, int aantal)
        {
            if (aantal <= 0)
            {
                var diners = ctx.Diners.Where(d => d.dinerID == dinerID);
                Diner updated = diners.First();
                ctx.Diners.Remove(updated);
                ctx.SaveChanges();
            }
            else
            {
                var diners = ctx.Diners.Where(d => d.dinerID == dinerID);
                Diner updated = diners.First();
                updated.aantal = aantal;

                ctx.Diners.Attach(updated);
                //db.Users.Attach(updatedUser);
                var entry = ctx.Entry(updated);
                //var entry = db.Entry(updatedUser);
                entry.Property(e => e.aantal).IsModified = true;
                // other changed properties
                ctx.SaveChanges();
            }
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
                mywishlistevenement.Add(new WishlistRepository().ctx.Evenementen.SingleOrDefault(e => e.evenementID == wishlistevent.evenementID));               
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
