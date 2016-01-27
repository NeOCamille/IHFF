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

        public ActionResult Woensdag()
        {
            return View(repository.GetDagprogramma("Woensdag"));
        }

        public ActionResult Donderdag()
        {
            return View(repository.GetDagprogramma("Donderdag"));
        }

        public ActionResult Vrijdag()
        {

            return View(repository.GetDagprogramma("Vrijdag"));
        }

        public ActionResult Zaterdag()
        {
            return View(repository.GetDagprogramma("Zaterdag"));
        }

        public ActionResult Zondag()
        {
            return View(repository.GetDagprogramma("Zondag"));
        }

        public ActionResult Popup(int id)
        {
            return View(repository.GetPopup(id));
        }

    }
}
