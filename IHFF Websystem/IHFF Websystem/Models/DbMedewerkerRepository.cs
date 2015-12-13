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

        public List<Wishlist> ShowData(Medewerker ingelogdeMedewerker)
        {
            List<Wishlist> managementList = new List<Wishlist>();

            if (ingelogdeMedewerker.relevantie == "Management")
            {
                foreach(Wishlist Wishlistentry in ctx.Wishlists)
                {
                    managementList.Add(Wishlistentry);
                }
            }

            return managementList;
        }
    }
}