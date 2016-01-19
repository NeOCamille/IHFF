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
            //check if excists or create list
            if (Session["wishlistEvenementList"] == null)
            {
                Session["wishlistEvenementList"] = new List<int>();
            }
            //Evenement MyEvent = new WishlistRepository().GetEvent(film.evenementID);

            //retrieve wishlist
            List<int> Mywishlist = Session["wishlistEvenementList"] as List<int>;

            //check if excist or create list
            //if (Session["wishlistEvenementList"] == null)
            //{
            //    Session["wishlistEvenementList"] = new List<int>();
            //}
            ////Evenement MyEvent = new WishlistRepository().GetEvent(film.evenementID);

            ////retrieve wishlist
            //List<int> Mywishlist = Session["wishlistEvenementList"] as List<int>;
//>>>>>>> 35c2b9686f6665692f02e553d8e3fa984da8a4a5
            
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
                    List<Diner> mywishlistdiner          = wishlistRepository.Getmywishlistdiner(wishlist.wishlistID);
                    //List<ViewWishlist> myevenements      = new List<ViewWishlist>();
                    //foreach (Evenement evenement in mywishlistevenements)
                    //{
                    //    ViewWishlist viewwishlist = new ViewWishlist();
                    //    viewwishlist.name = evenement.evenementNaam;
                    //    viewwishlist.starttijd = evenement.startTijd;
                    //    viewwishlist.locatie = wishlistRepository.GetLocatie(evenement.locatieID).locatieNaam;
                    //    viewwishlist.prijs = evenement.prijs.ToString();
                    //    viewwishlist.beschrijving = evenement.beschrijving;
                    //    viewwishlist.evenementID = wishlistRepository.GetWishlistEvenement(wishlist.wishlistID, evenement.evenementID).ID;
                    //    myevenements.Add(viewwishlist);
                    //}
                    //foreach (Diner diner in mywishlistdiner)
                    //{
                    //    ViewWishlist viewwishlist = new ViewWishlist();
                    //    viewwishlist.name = diner.opNaamVan;
                    //    viewwishlist.starttijd = diner.startTijd;
                    //    viewwishlist.eindttijd = diner.eindTijd;
                    //    viewwishlist.locatie = wishlistRepository.GetLocatie(diner.locatieID).locatieNaam;
                    //    viewwishlist.prijs = diner.prijs.ToString();
                    //    viewwishlist.dinerID = diner.dinerID;
                    //}
                    List<WishlistPopup> Popups = new List<WishlistPopup>();
                    foreach (Evenement evenement in mywishlistevenements)
                    {
                        WishlistPopup myPopup = new WishlistPopup();
                        myPopup.evenementID = evenement.evenementID;
                        myPopup.evenementNaam = evenement.evenementNaam;
                        myPopup.startTijd = evenement.startTijd;
                        myPopup.beschrijving = evenement.beschrijving;
                        myPopup.prijs = evenement.prijs;
                        myPopup.locatieID = evenement.locatieID;
                        myPopup.eventType = events.film;
                        myPopup.locatieNaam = wishlistRepository.GetLocatie(evenement.locatieID).locatieNaam;
                        Popups.Add(myPopup);
                    }
                    foreach (Diner diner in mywishlistdiner)
                    {
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
                        myPopup.locatieNaam = wishlistRepository.GetLocatie(diner.locatieID).locatieNaam;

                        Popups.Add(myPopup);
                    }
                    return View(Popups);
                }
            }
            return View();
        }
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
        public ActionResult WishlistPopUp()
        {
            if (Session["CurrentWishlist"] != null)
            {            
                int wishlistID = (int)Session["CurrentWishlist"];

                IEnumerable<Tuple<Film, int>> films = new WishlistRepository().GetAllWishlistFilms(wishlistID);
                IEnumerable<Tuple<Special, int>> specials = new WishlistRepository().GetAllWishlistSpecials(wishlistID);
                IEnumerable<Diner> diners     = new WishlistRepository().GetAllWishlistDiners(wishlistID);

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
                    myPopup.prijs = film.Item1.prijs;
                    myPopup.locatieID = film.Item1.locatieID;
                    myPopup.regisseur = film.Item1.regisseur;
                    myPopup.eventType = events.film;

                    myPopup.aantal = film.Item2;

                    Locatie locatie = wishlistRepository.GetLocatie(film.Item1.locatieID);
                    myPopup.locatieNaam = locatie.locatieNaam;

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
                    myPopup.prijs = special.Item1.prijs;
                    myPopup.locatieID = special.Item1.locatieID;
                    myPopup.onderwerp = special.Item1.onderwerp;
                    myPopup.spreker = special.Item1.spreker;
                    myPopup.eventType = events.special;

                    myPopup.aantal = special.Item2;

                    Locatie locatie = wishlistRepository.GetLocatie(special.Item1.locatieID);
                    myPopup.locatieNaam = locatie.locatieNaam;

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

                    myPopups.Add(myPopup);
                }

                //Popups.Sort((myy) => DateTime.Compare(x.Created, y.Created));
                
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


        //This handels mathijs his buttons
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
