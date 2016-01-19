using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHFF_Websystem.Models;

namespace IHFF_Websystem.Controllers
{
    public class HomeController : Controller
    {
        WishlistRepository repository = new WishlistRepository();
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Programma()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Locaties()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Dineren()
        {
            
            return View();
        }

        public ActionResult DinerReserveren(int id)
        {
            Session["CurrentLocatie"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult DinerReserveren(DinerReserverenModel dinermodel)
        {
            if (ModelState.IsValid)
            {
                if (Session["CurrentWishlist"] == null)
                {
                    int WishlistID = repository.NewWishlist().wishlistID;
                    Session["CurrentWishlist"] = WishlistID;
                }

                Diner diner = new Diner();
                diner.wishlistID = (int)Session["CurrentWishlist"];
                diner.locatieID = (int)Session["CurrentLocatie"];
                diner.startTijd = Convert.ToDateTime(dinermodel.StartTijd);
                diner.eindTijd = Convert.ToDateTime(dinermodel.EindTijd);
                diner.opNaamVan = dinermodel.OpNaamVan;
                diner.aantal = dinermodel.Aantal;
                diner.foodFilm = false;
                diner.prijs = 10.00*dinermodel.Aantal;
                repository.AddDiner(diner);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("error", "error");
            }
            return View();
        }

        public ActionResult Overons()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Sitemap()
        {

            return View();
        }
    }
}
