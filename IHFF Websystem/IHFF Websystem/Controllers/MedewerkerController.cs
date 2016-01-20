using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHFF_Websystem.Models;
using System.Data.Entity;
using System.Web.Security;

namespace IHFF_Websystem.Controllers
{
    public class MedewerkerController : Controller
    {
        private IMedewerkerRepository medewerkerRepository = new DbMedewerkerRepository();

        [Authorize]
        public ActionResult Index()
        {
            // Als er niemand ingelogd is, dan doorverwijzen naar login scherm, anders het medewerker startscherm
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            if (ingelogdeMedewerker != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [Authorize]
        public ActionResult AddMedewerker()
        {
            // Zoekt de locaties op die geselecteerd kunnen worden in de dropdownlist bij de registreerpagina
            List<Locatie> dropdownLocaties = medewerkerRepository.getLocaties();
            ViewBag.Locaties = dropdownLocaties;
            return View();
        }

        [HttpPost]
        public ActionResult AddMedewerker(Medewerker medewerker)
        {
            //Checkt of de ingelogde medewerker een manager is. Als dat zo is dan wordt er een nieuw account geregistreerd
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            if (ModelState.IsValid && ingelogdeMedewerker.locatieID == 19)
            {
                medewerkerRepository.AddMedewerker(medewerker);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "U bent niet bevoegd om accounts aan te maken";
                return View(medewerker);
            }
        }

        public ActionResult Login()
        {
            // Als er al een medewerker ingelogd is, krijgt diegene de medewerkerspagina te zien, anders het loginscherm
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            if (ingelogdeMedewerker != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(Login loginMedewerker)
        {
            // Checkt de credentials
            if (ModelState.IsValid)
            {
                Medewerker medewerker = medewerkerRepository.GetMedewerker(loginMedewerker.gebruikersNaam, loginMedewerker.passWord);
                if (medewerker != null)
                {
                    FormsAuthentication.SetAuthCookie(medewerker.gebruikersNaam, false);
                    Session["IngelogdeMedewerker"] = medewerker;
                    return RedirectToAction("Index", "Medewerker");
                }
                else
                {
                    ModelState.AddModelError("loginError", "Gebruikersnaam en wachtwoordcombinatie is fout");
                }
            }
            return View(loginMedewerker);
        }

        [Authorize]
        public ActionResult Uitlog()
        {
            // Uitloggen, nodig om signout te doen, en session.clear, anders dan redirect de "Medewerker" link niet goed
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult ShowWishlists()
        {
            //Checkt of de medewerker manager is, als dat zo is worden de wishlists getoond
            List<Wishlist> wishlistList = new List<Wishlist>();
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            if (ingelogdeMedewerker.locatieID == 19 || ingelogdeMedewerker.relevantie == "Management")
            {
                wishlistList = medewerkerRepository.GetWishlists(ingelogdeMedewerker);
                double totaalomzet = 0;
                foreach (var entry in wishlistList)
                {
                        totaalomzet = totaalomzet + entry.totaalPrijs;
                }
                ViewBag.Totaalomzet = totaalomzet;
            }
            else
            {
                ViewBag.Error = "U bent niet bevoegd om de wishlists te bekijken";
            }
            return View(wishlistList); 
        }

        [Authorize]
        public ActionResult DeleteWishlist(int? wishlistID)
        {
            // Checkt of de medewerker een manager is, dan wordt de wishlist verwijderd
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            if (ModelState.IsValid && ingelogdeMedewerker.locatieID == 19)
            {
                medewerkerRepository.DeleteWishlist(wishlistID);
                return RedirectToAction("ShowWishlists", "Medewerker");
            }
            return View();
        }

        [Authorize]
        public ActionResult DeleteReservering(int? dinerID)
        {
            if (ModelState.IsValid)
            {
                medewerkerRepository.DeleteReservering(dinerID);
                return RedirectToAction("ShowReserveringen", "Medewerker");
            }
            return View();
        }

        [Authorize]
        public ActionResult EditWishlist(int? wishlistID)
        {
            // Checkt of medewerker een manager is, dan wordt de gewenste wishlist opgezocht
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            Wishlist wishlist;
            if (ingelogdeMedewerker.locatieID == 19)
            {
                wishlist = medewerkerRepository.EditWishlistID(wishlistID);
            }
            else
            {
                wishlist = null;
            }
                return View(wishlist);
        }
        
        [HttpPost]
        public ActionResult EditWishlist(Wishlist newWishlist)
        {
            // Checkt of medewerker een manager is, verandert daarna de wishlist
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            if (ModelState.IsValid && ingelogdeMedewerker.locatieID == 19)
            {
                medewerkerRepository.EditWishlist(newWishlist);
                return RedirectToAction("ShowWishlists", "Medewerker");
            }
            return View();
        }

        [Authorize]
        public ActionResult ShowReserveringen()
        {
            // Als de medewerker een restauranteigenaar is, worden alleen de relevante reserveringen getoond
            // Als de medewerker een manager is, worden alle reserveringen getoond
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            if (ModelState.IsValid)
            {
                List<Diner> reserveringsList = medewerkerRepository.GetReserveringen(ingelogdeMedewerker);
                return View(reserveringsList);
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult ShowAgenda()
        {
            // Checkt of de medewerker een manager is, en laat dan lijsten met films en specials zien
            List<Special> specialsList = new List<Special>();
            List<Film> filmsList = new List<Film>();

            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            if (ModelState.IsValid && ingelogdeMedewerker.locatieID == 19)
            {
                specialsList = medewerkerRepository.GetSpecials(ingelogdeMedewerker);
                filmsList = medewerkerRepository.GetFilms(ingelogdeMedewerker);
                ViewBag.Films = filmsList;
                return View(specialsList); 
            }
            else
            {
                ViewBag.Films = filmsList;
                ViewBag.Error = "U bent niet bevoegd deze pagina te bekijken.";
                return View(specialsList);
            }
        }
   
        public ActionResult Instellingen()
        {
            return View();
        }

        public ActionResult DeleteAccount()
        {
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];

            medewerkerRepository.DeleteAccount(ingelogdeMedewerker.medewerkerID);
            FormsAuthentication.SignOut();
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
