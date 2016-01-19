using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IHFF_Websystem.Models
{
    public class DbMedewerkerRepository : IMedewerkerRepository
    {
       public IHFFContext ctx = new IHFFContext();

       public void AddMedewerker(Medewerker medewerker, Medewerker ingelogdeMedewerker)
       {
               ctx.Medewerkers.Add(medewerker);
               ctx.SaveChanges();
       }

        public Medewerker GetMedewerker(string gebruikersNaam, string passWord)
       {
           Medewerker gevondenMedewerker = ctx.Medewerkers.SingleOrDefault(m => m.gebruikersNaam == gebruikersNaam && m.passWord == passWord);
           return gevondenMedewerker;
       }

        public List<Wishlist> ShowDataManagement(Medewerker ingelogdeMedewerker)
        {
            if (ingelogdeMedewerker.locatieID == 19 || ingelogdeMedewerker.relevantie == "Management")
            {
                List<Wishlist> wishlistList = new List<Wishlist>();

                foreach (Wishlist Wishlistentry in ctx.Wishlists)
                {
                    wishlistList.Add(Wishlistentry);
                }
                return wishlistList;
            }
            else {
                List<Wishlist> wishlistList = new List<Wishlist>();

                return wishlistList;
            }

        }

        public List<Diner> ShowReserveringen(Medewerker ingelogdeMedewerker)
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
    
// raaaaaaaaaaaaaaaaaaaaaav
// PLS HELP
// raaaaaaaaaaaaaaaaaaaaaav

        public void DeleteWishlist(int? wishlistID)
        {
            Wishlist wishlist = ctx.Wishlists.Find(wishlistID);
            ctx.Wishlists.Remove(wishlist);
            //ctx.SaveChanges();

            foreach (WishlistEvenement entry in ctx.WishlistEvenements)
            {
                WishlistEvenement wishlistEvenement = ctx.WishlistEvenements.Find(wishlistID);
                if (wishlistEvenement != null)
                {
                    ctx.WishlistEvenements.Remove(wishlistEvenement);
                }
                //ctx.SaveChanges();
            }

            foreach (Diner entry in ctx.Diners)
            {
                Diner diner = ctx.Diners.Find(wishlistID);
                if (diner != null)
                {
                    ctx.Diners.Remove(diner);
                }
                //ctx.SaveChanges();
            }

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

        public List<Special> ShowSpecials(Medewerker ingelogdeMedewerker)
        {
            List<Special> specials = new List<Special>();

            if (ingelogdeMedewerker.relevantie == "Management" || ingelogdeMedewerker.locatieID == 19)
            {
                foreach (Special entry in ctx.Specials)
                {
                    specials.Add(entry);
                }
            }

            return specials;

        }

        public List<Film> ShowFilms(Medewerker ingelogdeMedewerker)
        {
            List<Film> films = new List<Film>();

            if (ingelogdeMedewerker.locatieID == 19 || ingelogdeMedewerker.relevantie == "Management")
            {
                foreach (Film entry in ctx.Films)
                {
                    films.Add(entry);
                }
            }

            return films;
        }
    }
}