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
        // eventList met alle events voor programma?
        //List<Evenement> events;
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

        public void Popup(int i)
        {
            //Switch huidig popup
            //popup.evenement = (Evenement)events.Where(e => e.evenementID == i);
        }

    }
}
