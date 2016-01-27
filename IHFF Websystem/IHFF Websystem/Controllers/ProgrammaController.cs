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
            List<Popup> popups = repository.GetDagprogramma("Woensdag");
            return View(popups);
        }

        public ActionResult Donderdag()
        {
            List<Popup> popups = repository.GetDagprogramma("Donderdag");
            return View(popups);
        }

        public ActionResult Vrijdag()
        {
            List<Popup> popups = repository.GetDagprogramma("Vrijdag");
            return View(popups);
        }

        public ActionResult Zaterdag()
        {
            List<Popup> popups = repository.GetDagprogramma("Zaterdag");
            return View(popups);
        }

        public ActionResult Zondag()
        {
            List<Popup> popups = repository.GetDagprogramma("Zondag");
            return View(popups);
        }

        public ActionResult Popup(int id)
        {
            
            //Switch huidig popup
            popup.evenement = repository.GetEvent(id);
            //popup = new Popup(repository.GetEvent(id));
            popup.datum = String.Format("{0:dd-MM-yyyy}", popup.evenement.startTijd);
            popup.tijd = String.Format("{0:HH:mm}", popup.evenement.startTijd);
            popup.locatieNaam = (string)repository.GetLocatie(popup.evenement.locatieID).locatieNaam;
            return View(popup);
        }

    }
}
