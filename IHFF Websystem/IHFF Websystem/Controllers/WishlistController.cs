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
        private IWishlistRepository wishlistRepository = new WishlistRepository();
        //
        // GET: /Wishlist/

        public ActionResult Index()
        {
            //check of er een wishlist gebruikt word
            if (Session["CurrentWishlist"] != null)
            {
                //get huidige wishlistID uit sessievariabe
                int wishlistID = (int)Session["CurrentWishlist"];

                //verkrijg alle evenementen uit de wishlist
                //de tuple word gebruikt om het aantal mee te krijgen, diners heeft al een aantal vanzichzelf
                IEnumerable<Tuple<Film, int>> films = new WishlistRepository().GetAllWishlistFilms(wishlistID);
                IEnumerable<Tuple<Special, int>> specials = new WishlistRepository().GetAllWishlistSpecials(wishlistID);
                IEnumerable<Diner> diners = new WishlistRepository().GetAllWishlistDiners(wishlistID);

                //voeg alle evenementen samen in een list
                List<WishlistPopup> myPopups = mergeEvents(films, specials, diners);

                ViewData["wishlistid"] = wishlistID;

                //order de list chronlogish
                return View(myPopups.OrderBy(x => x.startTijd).ToList());
            }
            else
            {
                return View();
            }
        }
        
        [HttpPost]
        public ActionResult WishlistLaden(string codeword)
        {
            Wishlist wishlist = null;
            try
            {
                wishlist = wishlistRepository.GetWishList(codeword);
            }
            catch (Exception ex)
            {

            }
            if (wishlist != null)
            {
                Session["CurrentWishlist"] = wishlist.wishlistID;
                //return RedirectToAction("Index");
                return new EmptyResult();
            }
            return View();
        }

        [HttpPost]
        public ActionResult WishlistOpslaan(string codeword)
        {
            if (Session["CurrentWishlist"] != null)
            {
                int wishlistID = (int)Session["CurrentWishlist"];
                try
                {
                    wishlistRepository.UpdatecodeWoord(wishlistID, codeword);
                }
                catch (Exception ex)
                {

                }
                //return RedirectToAction("Index");
                return new EmptyResult();
                
            }
            return new EmptyResult();
        }
        public ActionResult WishlistPopUp()
        {
            if (Session["CurrentWishlist"] != null)
            {            
                int wishlistID = (int)Session["CurrentWishlist"];

                IEnumerable<Tuple<Film, int>> films = new WishlistRepository().GetAllWishlistFilms(wishlistID);
                IEnumerable<Tuple<Special, int>> specials = new WishlistRepository().GetAllWishlistSpecials(wishlistID);
                IEnumerable<Diner> diners     = new WishlistRepository().GetAllWishlistDiners(wishlistID);

                List<WishlistPopup> myPopups = mergeEvents(films, specials, diners);

                return View(myPopups.OrderBy(x => x.startTijd).ToList());
            }
            else
            {
                return new EmptyResult();
            }
        }
        

        // GET: /Wishlist/AddTo
        public ActionResult AddTo()
        {
            
            return View();
        }

        //Debug code
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
            new WishlistRepository().AddDiner(diner);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult AddFilmToWishlist(Film film)
        {

            return RedirectToAction("Index", "Home");
        }


        //This handels matthijs his buttons
        [HttpPost]
        public ActionResult AddEvenementToWishlist(int id, int aantal)
        {
            //only create a new wishlist if non exists
            if (Session["CurrentWishlist"] == null)
            {
                int WishlistID = wishlistRepository.NewWishlist().wishlistID;
                Session["CurrentWishlist"] = WishlistID;
            }

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
        //Update aantal value
        [HttpPost]
        public ActionResult UpdateAantal(int eID, int aantal, bool diner)
        {
            if (diner)
            {
                wishlistRepository.UpdateAantal_D(eID, aantal);
            }
            else
            {
                int wID = (int)Session["CurrentWishlist"]; //You only come here if session is set
                wishlistRepository.UpdateAantal_WE(eID, wID, aantal);
            }
            return new EmptyResult();
        }


        public ActionResult AddEvenementToWishlist(Evenement evenement)
        {
            return View();
        }

        public ActionResult DeleteWishlist(int wishlistid)
        {
            Wishlist wishlist = wishlistRepository.GetWishList(wishlistid);
            wishlistRepository.GetWishlistTotalPrice(wishlistid);
            return View(wishlist);
        }

        [HttpPost]
        public ActionResult DeleteWishlist(Wishlist wishlist)
        {
            Session["CurrentWishlist"] = null;
            wishlistRepository.DeleteWishlist(wishlist);
            return RedirectToAction("Index");
        }

        public ActionResult Reserveren(int wishlistid)
        {
            //check of er een wishlist word gebruikt
            if (Session["CurrentWishlist"] != null)
            {
                wishlistRepository.GetWishlistTotalPrice(wishlistid);
                Wishlist wishlist = wishlistRepository.GetWishList((int)Session["CurrentWishlist"]);

                return View(wishlist);
            }
            //geen wishlist dan naar wishlist pagina
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Reserveren(Wishlist wishlist)
        {
            wishlistRepository.WishListReserveren(wishlist);
            return RedirectToAction("Index");      
        }

        // ### Functions ###
        public List<WishlistPopup> mergeEvents(IEnumerable<Tuple<Film, int>> films, IEnumerable<Tuple<Special, int>> specials, IEnumerable<Diner> diners)
        {
            List<WishlistPopup> myPopups = new List<WishlistPopup>();

            foreach (Tuple<Film, int> film in films)
            {
                if (film.Item1 == null)
                    continue;

                WishlistPopup myPopup = new WishlistPopup();

                myPopup.evenementID = film.Item1.evenementID;
                myPopup.evenementNaam = film.Item1.evenementNaam;
                myPopup.startTijd = film.Item1.startTijd;
                myPopup.beschrijving = film.Item1.beschrijving;
                myPopup.prijs = film.Item1.prijs * film.Item2;
                myPopup.locatieID = film.Item1.locatieID;
                myPopup.regisseur = film.Item1.regisseur;
                myPopup.eventType = events.film;

                myPopup.aantal = film.Item2;

                Locatie locatie = wishlistRepository.GetLocatie(film.Item1.locatieID);
                myPopup.locatieNaam = locatie.locatieNaam;

                int plaatsen = wishlistRepository.CheckAvailabilityEvenement(film.Item1.evenementID);
                myPopup.plaatsenVrij = plaatsen;

                myPopups.Add(myPopup);
            }

            foreach (Tuple<Special, int> special in specials)
            {
                if (special.Item1 == null)
                    continue;

                WishlistPopup myPopup = new WishlistPopup();

                myPopup.evenementID = special.Item1.evenementID;
                myPopup.evenementNaam = special.Item1.evenementNaam;
                myPopup.startTijd = special.Item1.startTijd;
                myPopup.beschrijving = special.Item1.beschrijving;
                myPopup.prijs = special.Item1.prijs * special.Item2;
                myPopup.locatieID = special.Item1.locatieID;
                myPopup.onderwerp = special.Item1.onderwerp;
                myPopup.spreker = special.Item1.spreker;
                myPopup.eventType = events.special;

                myPopup.aantal = special.Item2;

                Locatie locatie = wishlistRepository.GetLocatie(special.Item1.locatieID);
                myPopup.locatieNaam = locatie.locatieNaam;

                int plaatsen = wishlistRepository.CheckAvailabilityEvenement(special.Item1.evenementID);
                myPopup.plaatsenVrij = plaatsen;

                myPopups.Add(myPopup);
            }

            foreach (Diner diner in diners)
            {
                if (diner == null)
                    continue;

                WishlistPopup myPopup = new WishlistPopup();

                myPopup.dinerID = diner.dinerID;
                myPopup.startTijd = diner.startTijd;
                myPopup.eindTijd = diner.eindTijd;
                myPopup.foodFilm = diner.foodFilm;
                myPopup.opNaamVan = diner.opNaamVan;
                myPopup.prijs = diner.prijs;
                myPopup.wishlistID = diner.wishlistID;
                myPopup.locatieID = diner.locatieID;
                myPopup.aantal = diner.aantal;
                myPopup.eventType = events.diner;

                Locatie locatie = wishlistRepository.GetLocatie(diner.locatieID);
                myPopup.locatieNaam = locatie.locatieNaam;

                int plaatsen = wishlistRepository.CheckAvailabilityDiner(diner.dinerID);
                myPopup.plaatsenVrij = plaatsen;

                myPopups.Add(myPopup);
            }
            return myPopups;
        }

        // ## Overige methode
        public ActionResult UpdateEvenementToWishlist(int id, int aantal)
        {
            if (Session["CurrentWishlist"] != null)
            {
                int wishlistID = (int)Session["CurrentWishlist"];



                return new EmptyResult();
            }
            else
            {
                return new EmptyResult();
            }
        }
        public ActionResult Create()
        {
            wishlistRepository.CheckAvailabilityEvenement(8);
            //if (Session["CurrentWishlist"] == null)
            //{
            //test line
            //int WishlistID = wishlistRepository.NewWishlist().wishlistID; // 1;//new Random().Next(0,100000);
            //Session["CurrentWishlist"] = WishlistID;
            //}

            IEnumerable<Film> films = new WishlistRepository().GetAllFilms();

            return View(films);
        }
    }
}
