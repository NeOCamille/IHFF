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
        private WishlistRepository wishlistRepository = new WishlistRepository();
        //
        // GET: /Wishlist/

        public ActionResult Index()
        {
<<<<<<< HEAD
            //check if excists or create list
            if (Session["wishlistEvenementList"] == null)
            {
                Session["wishlistEvenementList"] = new List<int>();
            }
            //Evenement MyEvent = new WishlistRepository().GetEvent(film.evenementID);

            //retrieve wishlist
            List<int> Mywishlist = Session["wishlistEvenementList"] as List<int>;
=======
            //check if excist or create list
            //if (Session["wishlistEvenementList"] == null)
            //{
            //    Session["wishlistEvenementList"] = new List<int>();
            //}
            ////Evenement MyEvent = new WishlistRepository().GetEvent(film.evenementID);

            ////retrieve wishlist
            //List<int> Mywishlist = Session["wishlistEvenementList"] as List<int>;
>>>>>>> 35c2b9686f6665692f02e553d8e3fa984da8a4a5
            
            //List<ViewWishlist> MyList = new List<ViewWishlist>();

            //foreach (int id in Mywishlist)
            //{
            //    ViewWishlist item = new ViewWishlist();
                
            //    Evenement MyEvent = new WishlistRepository().GetEvent(id);
            //    item.name = MyEvent.evenementNaam;
            //    item.starttijd = MyEvent.startTijd.ToString("dd MM yyyy HH:mm");
            //    item.prijs = MyEvent.prijs.ToString();
            //    item.beschrijving = MyEvent.beschrijving;
            //    item.locatie = new WishlistRepository().GetLocatie(MyEvent.locatieID).locatieNaam;
            //    item.regiseur = new WishlistRepository().GetFilm(MyEvent.evenementID).regisseur;
            //    MyList.Add(item);
            //}

            //ViewBag.Mylist = MyList;

            //return View(MyList);

            if (Session["CurrentWishlist"] != null)
            {
                Wishlist wishlist = wishlistRepository.GetWishList((int)Session["CurrentWishlist"]);
                if (wishlist != null)
                {
                    List<Evenement> mywishlistevenements = wishlistRepository.GetMyWishlistEvenements(wishlist.wishlistID);
                    List<Diner> mywishlistdiner = wishlistRepository.Getmywishlistdiner(wishlist.wishlistID);
                    List<ViewWishlist> myevenements = new List<ViewWishlist>();
                    foreach (Evenement evenement in mywishlistevenements)
                    {
                        ViewWishlist viewwishlist = new ViewWishlist();
                        viewwishlist.name = evenement.evenementNaam;
                        viewwishlist.starttijd = evenement.startTijd;
                        viewwishlist.locatie = wishlistRepository.GetLocatie(evenement.locatieID).locatieNaam;
                        viewwishlist.prijs = evenement.prijs.ToString();
                        viewwishlist.beschrijving = evenement.beschrijving;
                        viewwishlist.evenementID = wishlistRepository.GetWishlistEvenement(wishlist.wishlistID, evenement.evenementID).ID;
                        myevenements.Add(viewwishlist);
                    }
                    foreach (Diner diner in mywishlistdiner)
                    {
                        ViewWishlist viewwishlist = new ViewWishlist();
                        viewwishlist.name = diner.opNaamVan;
                        viewwishlist.starttijd = diner.startTijd;
                        viewwishlist.eindttijd = diner.eindTijd;
                        viewwishlist.locatie = wishlistRepository.GetLocatie(diner.dinerID).locatieNaam;
                        viewwishlist.prijs = diner.prijs.ToString();
                        viewwishlist.dinerID = diner.dinerID;
                    }
                    return View(myevenements);
                }
            }
            return View();
        }

        [HttpPost,ActionName("Index")]
        public ActionResult WishlistLaden(string codeword)
        {
            Wishlist wishlist = wishlistRepository.GetWishList(codeword);
            if (wishlist != null)
            {
                Session["CurrentWishlist"] = wishlist.wishlistID;
                return RedirectToAction("Index");
            }

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

            IEnumerable<Film> films       = new WishlistRepository().GetAllWishlistFilms(wishlistID);
            IEnumerable<Special> specials = new WishlistRepository().GetAllWishlistSpecials(wishlistID);
            IEnumerable<Diner> diners     = new WishlistRepository().GetAllWishlistDiners(wishlistID);

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
        public ActionResult AddEvenementToWishlist(int id, uint aantal)
        {
            // get current wishlist ID
            int wishlistID = (int)Session["CurrentWishlist"];
            // get evenementID form postdata
            int evenementID = id;

            //save event to wishlist in DB
            new WishlistRepository().AddEvenement(wishlistID, evenementID, aantal);

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


        [HttpPost]
        public ActionResult DeleteEvenement(int id)
        {
            wishlistRepository.DeleteWishlistEvenement(id);
            return RedirectToAction("Index"); 
        }

        [HttpPost]
        public ActionResult DeleteDiner(int id)
        {
            wishlistRepository.DeleteDiner(id);
            return RedirectToAction("Index");
        }

        public ActionResult Reserveren()
        {
                Wishlist wishlist = wishlistRepository.GetWishList((int)Session["CurrentWishlist"]);
                return View(wishlist);
        }

        [HttpPost,ActionName("Reserveren")]
        public ActionResult ReserverenPost(Wishlist wishlist)
        {
            wishlistRepository.WishListReserveren(wishlist);
            return RedirectToAction("Index");      
        }
    }
}
