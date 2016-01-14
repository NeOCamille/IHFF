using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHFF_Websystem.Models;

namespace IHFF_Websystem.Controllers
{
    public class ProgrammaController : Controller
    {
        WishlistRepository repository = new WishlistRepository();
        Popup popup = new Popup();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Woensdag()
        {
            return View(popup);
        }

        public ActionResult Donderdag()
        {
            return View(popup);
        }

        public ActionResult Vrijdag()
        {
            return View(popup);
        }

        public ActionResult Zaterdag()
        {
            return View(popup);
        }

        public ActionResult Zondag()
        {
            return View(popup);
        }

        public ActionResult Popup(int id, string url)
        {
            //Switch huidig popup
            popup.evenement = repository.GetEvent(id);
            popup.locatieNaam = repository.GetLocatie(popup.evenement.locatieID).locatieNaam;
            return View(popup);
        }

    }
}
