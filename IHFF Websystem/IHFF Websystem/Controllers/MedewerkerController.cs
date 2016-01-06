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

        public ActionResult Index()
        {
            return View();
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
            if (ModelState.IsValid)
            {
                medewerkerRepository.AddMedewerker(medewerker);
                return RedirectToAction("Index");
            }
            return View(medewerker);
        }

        public ActionResult Login()
        {
            return View();
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

        public ActionResult Uitlog()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Medewerker");
        }

        [Authorize]
        public ActionResult ShowData()
        {
            Medewerker ingelogdeMedewerker = (Medewerker)Session["IngelogdeMedewerker"];
            if (ingelogdeMedewerker.relevantie == "Management")
            {
                List<Wishlist> wishlistList = medewerkerRepository.ShowDataManagement(ingelogdeMedewerker);

                return View(wishlistList);
            }
            return View();
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
                List<Diner> reserveringsList = medewerkerRepository.ShowDataDiners(ingelogdeMedewerker);
                return View(reserveringsList);
            }

            return View();

        }
        
    }
}
