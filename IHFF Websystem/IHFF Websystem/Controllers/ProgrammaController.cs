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

        public ActionResult Woensdag()
        {
            return View();
        }

        public ActionResult Donderdag()
        {
            return View();
        }

        public ActionResult Vrijdag()
        {
            return View();
        }

        public ActionResult Zaterdag()
        {
            return View();
        }

        public ActionResult Zondag()
        {
            return View();
        }

        public ActionResult Popup(int id, string url)
        {
            //Switch huidig popup
            popup.evenement = repository.GetEvent(id);
            popup.datum = String.Format("{0:dd-MM-yyyy}", popup.evenement.startTijd);
            popup.tijd = String.Format("{0:HH:mm}", popup.evenement.startTijd);
            popup.locatieNaam = repository.GetLocatie(popup.evenement.locatieID).locatieNaam;
            return View(popup);
        }

    }
}
