using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHFF_Websystem.Models;

namespace IHFF_Websystem.Controllers
{
    public class WishlistController : Controller
    {
        //
        // GET: /Wishlist/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            if (Session["CurrentWishlist"] == null)
            {
                //test line
                int WishlistID = new Random().Next();
                Session["CurrentWishlist"] = WishlistID;
            }
            return View();
        }
        

        // GET: /Wishlist/AddTo
        public ActionResult AddTo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDinerToWishlist(Diner diner)
        {
            //wishlist id in session?
            int WishlistID = (int)Session["CurrentWishlist"];

            diner.dinerID = 1;
            diner.startTijd = DateTime.Now;
            diner.eindTijd = DateTime.Now.AddHours(2);
            diner.foodFilm = false;
            diner.opNaamVan = "Schravesande";
            diner.prijs = 66.6D;
            diner.locatieID = 1;
            diner.wishlistID = 1;

            //add directly to DB
            new WishlistRepository().AddDiner(WishlistID, diner);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult AddFilmToWishlist(Film film)
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEvenementToWishlist(Evenement evenement)
        {
            return View();
        }

    }
}
