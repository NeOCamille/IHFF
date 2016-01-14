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

        public List<Wishlist> ShowDataManagement(Medewerker ingelogdeMedewerker)
        {
            List<Wishlist> managementList = new List<Wishlist>();

                foreach(Wishlist Wishlistentry in ctx.Wishlists)
                {
                    managementList.Add(Wishlistentry);
                }

            return managementList;
        }

        public List<Diner> ShowDataDiners(Medewerker ingelogdeMedewerker)
        {
            List<Diner> dinerList = new List<Diner>();
            if (ingelogdeMedewerker.locatieID != 0 || ingelogdeMedewerker.relevantie != "Management")
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

        public List<Evenement> ShowEvenementen(Medewerker ingelogdeMedewerker)
        {
            List<Evenement> evenementen = new List<Evenement>();

            if (ingelogdeMedewerker.relevantie == "Management" || ingelogdeMedewerker.locatieID == 0)
            {
                foreach (Evenement entry in ctx.Evenementen)
                {
                    evenementen.Add(entry);
                }
            }

            return evenementen;

        }
    }
}