using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IHFF_Websystem.Models
{
    public class DbMedewerkerRepository : IMedewerkerRepository
    {
       public IHFFContext ctx = new IHFFContext();

       public void AddMedewerker(Medewerker medewerker)
       {
               ctx.Medewerkers.Add(medewerker);
               ctx.SaveChanges();
       }

        public Medewerker GetMedewerker(string gebruikersNaam, string passWord)
       {
           Medewerker gevondenMedewerker = ctx.Medewerkers.SingleOrDefault(m => m.gebruikersNaam == gebruikersNaam && m.passWord == passWord);
           return gevondenMedewerker;
       }

        public List<Wishlist> GetWishlists(Medewerker ingelogdeMedewerker)
        {
            List<Wishlist> wishlistList = new List<Wishlist>();
            foreach (Wishlist Wishlistentry in ctx.Wishlists)
            {
                wishlistList.Add(Wishlistentry);
            }
            return wishlistList;
        }
        

        public List<Diner> GetReserveringen(Medewerker ingelogdeMedewerker)
        {
            List<Diner> dinerList = new List<Diner>();
            if (ingelogdeMedewerker.locatieID != 19 || ingelogdeMedewerker.relevantie != "Management")
            {
                foreach (Diner dinerEntry in ctx.Diners)
                {
                    if (dinerEntry.locatieID == ingelogdeMedewerker.locatieID)
                    {
                        dinerList.Add(dinerEntry);
                    }
                }
            }
            else
            {
                foreach (Diner dinerEntry in ctx.Diners)
                {
                    dinerList.Add(dinerEntry);
                }
            }
            return dinerList;
        }

        public void DeleteWishlist(int? wishlistID)
        {
            Wishlist wishlist = ctx.Wishlists.Find(wishlistID);
            ctx.Wishlists.Remove(wishlist);
            ctx.SaveChanges();
        }

        public Wishlist EditWishlistID(int? wishlistID)
        {
            Wishlist wishlist = ctx.Wishlists.Find(wishlistID);
            return wishlist;
        }

        public void EditWishlist(Wishlist wishlist)
        {
            ctx.Entry(wishlist).State = System.Data.EntityState.Modified;
            ctx.SaveChanges();
        }

        public void DeleteReservering(int? dinerID)
        {
            Diner diner = ctx.Diners.Find(dinerID);
            ctx.Diners.Remove(diner);
            ctx.SaveChanges();
        }

        public List<Special> GetSpecials(Medewerker ingelogdeMedewerker)
        {
            List<Special> specials = new List<Special>();
            foreach (Special entry in ctx.Specials)
            {
                specials.Add(entry);
            }
            return specials;
        }

        public List<Film> GetFilms(Medewerker ingelogdeMedewerker)
        {
            List<Film> films = new List<Film>();
            foreach (Film entry in ctx.Films)
            {
                films.Add(entry);
            }
            return films;
        }

        public List<Locatie> getLocaties()
        {
            List<Locatie> locaties = new List<Locatie>();
            foreach (Locatie entry in ctx.Locaties)
            {
                if (entry.locatieID == 1 || entry.locatieID == 3 || entry.locatieID == 4 || entry.locatieID == 5 || entry.locatieID == 6 || entry.locatieID == 7 || entry.locatieID == 19)
                {
                    locaties.Add(entry);
                }
            }
            return locaties;
        }

        public void DeleteAccount(int medewerkerID)
        {
            Medewerker medewerker = ctx.Medewerkers.Find(medewerkerID);
            ctx.Medewerkers.Remove(medewerker);
            ctx.SaveChanges();
        }
    }
}