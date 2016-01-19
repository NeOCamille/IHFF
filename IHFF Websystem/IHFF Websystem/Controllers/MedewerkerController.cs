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
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddMedewerker(Medewerker medewerker)
        {
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            if (ModelState.IsValid && ingelogdeMedewerker.locatieID == 19)
            {
                medewerkerRepository.AddMedewerker(medewerker, ingelogdeMedewerker);
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
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult ShowData()
        {
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            List<Wishlist> wishlistList = medewerkerRepository.ShowDataManagement(ingelogdeMedewerker);

            if (ingelogdeMedewerker.relevantie != "Management" || ingelogdeMedewerker.locatieID != 19)
            {
                ViewBag.Error = "U bent niet bevoegd om de wishlists te bekijken";
            }
            double totaalomzet = 0;

            foreach (var entry in wishlistList)
            {
                if (entry.isBetaald == true)
                {
                    totaalomzet = totaalomzet + entry.totaalPrijs;
                }
            }
            ViewBag.Totaalomzet = totaalomzet;

            //List<WishlistEvenement> wishlistKoppelingen = new List<WishlistEvenement>();
            //
            //
            return View(wishlistList);   
            
        }


        [Authorize]
        public ActionResult DeleteWishlist(int? wishlistID)
        {
            if (ModelState.IsValid)
            {
                medewerkerRepository.DeleteWishlist(wishlistID);
                return RedirectToAction("ShowData", "Medewerker");
            }
            return View();
        }

        [Authorize]
        public ActionResult DeleteReservering(int? dinerID)
        {
            if (ModelState.IsValid)
            {
                medewerkerRepository.DeleteReservering(dinerID);
                return RedirectToAction("GetReserveringen", "Medewerker");
            }

            return View();
        }
        [Authorize]
        public ActionResult EditWishlist(int? wishlistID)
        {
            Wishlist wishlist = medewerkerRepository.EditWishlistID(wishlistID);
            return View(wishlist);
        }
        
        [HttpPost]
        public ActionResult EditWishlist(Wishlist newWishlist)
        {
            if (ModelState.IsValid)
            {
                medewerkerRepository.EditWishlist(newWishlist);
                return RedirectToAction("ShowData", "Medewerker");
            }

            return View();
        }
        [Authorize]
        public ActionResult GetReserveringen()
        {
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            if (ModelState.IsValid)
            {
                List<Diner> reserveringsList = medewerkerRepository.ShowReserveringen(ingelogdeMedewerker);
                return View(reserveringsList);
            }
            else
            {
                return View();
            }
            

        }

        [Authorize]
        public ActionResult GetAgenda()
        {
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            if (ModelState.IsValid)
            {
                List<Special> specialsList = medewerkerRepository.ShowSpecials(ingelogdeMedewerker);
                List<Film> filmsList = medewerkerRepository.ShowFilms(ingelogdeMedewerker);
                ViewBag.Films = filmsList;
                return View(specialsList);
                
            }
            else
            {
                return View();
            }
        }
        
    }
}
