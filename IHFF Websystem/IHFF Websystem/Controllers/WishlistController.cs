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
            //check if excist or create list
            if (Session["wishlistEvenementList"] == null)
            {
                Session["wishlistEvenementList"] = new List<int>();
            }
            //Evenement MyEvent = new WishlistRepository().GetEvent(film.evenementID);

            //retrieve wishlist
            List<int> Mywishlist = Session["wishlistEvenementList"] as List<int>;
            
            List<ViewWishlist> MyList = new List<ViewWishlist>();

            foreach (int id in Mywishlist)
            {
                ViewWishlist item = new ViewWishlist();
                
                Evenement MyEvent = new WishlistRepository().GetEvent(id);
                item.name = MyEvent.evenementNaam;
                item.starttijd = MyEvent.startTijd.ToString("dd MM yyyy HH:mm");
                item.prijs = MyEvent.prijs.ToString();
                item.beschrijving = MyEvent.beschrijving;
                item.locatie = new WishlistRepository().GetLocatie(MyEvent.locatieID).locatieNaam;
                item.regiseur = new WishlistRepository().GetFilm(MyEvent.evenementID).regisseur;
                MyList.Add(item);
            }

            ViewBag.Mylist = MyList;

            return View();
        }
        public ActionResult Create()
        {
            if (Session["CurrentWishlist"] == null)
            {
                //test line
                int WishlistID = new Random().Next(0,100000);
                Session["CurrentWishlist"] = WishlistID;
            }

            IEnumerable<Film> films = new WishlistRepository().GetAllFilms();

            return View(films);
        }
        public ActionResult WishlistPopUp()
        {
            if (Session["CurrentWishlist"] == null)
            {
                //test line
                int WishlistID = new Random().Next(0, 100000);
                Session["CurrentWishlist"] = WishlistID;
            }

            int wishlistID = (int)Session["CurrentWishlist"];

            IEnumerable<Film> films = new WishlistRepository().GetAllWishlistFilms(wishlistID);

            return View(films);
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

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult AddEvenementToWishlist(int id)
        {
            // get current wishlist ID
            int wishlistID = (int)Session["CurrentWishlist"];
            // get evenementID form postdata
            int evenementID = id;

            //save event to wishlist in DB
            new WishlistRepository().AddEvenement(wishlistID, evenementID);

            //don't return anyting because a background post handles this
            return new EmptyResult();
        }

        //test to add events
        [HttpPost]
        public ActionResult CreateFilm(string evenementNaam, DateTime startTijd, string beschrijving, double prijs, string regisseur, int locatieID)
        {
            //save event to wishlist in DB
            new WishlistRepository().CreateFilm(evenementNaam, startTijd, beschrijving, prijs, regisseur, locatieID);
            //don't return anyting because a background post handles this
            return new EmptyResult();
        }
        public ActionResult AddEvenementToWishlist(Evenement evenement)
        {
            return View();
        }

    }
}
