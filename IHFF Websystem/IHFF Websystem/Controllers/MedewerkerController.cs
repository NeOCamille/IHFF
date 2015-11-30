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

        public ActionResult AddMedewerker()
        {
            return View();
        }
       
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

    }
}
