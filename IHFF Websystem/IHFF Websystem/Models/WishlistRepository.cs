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
    }
}
