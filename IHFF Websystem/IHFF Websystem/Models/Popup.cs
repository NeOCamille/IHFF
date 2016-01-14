using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IHFF_Websystem.Controllers;

namespace IHFF_Websystem.Models
{
    public class Popup
    {
        public Evenement evenement { get; set; }
        public string locatieNaam { get; set; }
        public string tijd { get; set; }
        public string datum { get; set; }

        public Popup()
        {
            //TESTCODE (moet uiteindelijk ui db eventList komen.)
            evenement = new Film();

            evenement.evenementNaam = "";
            evenement.beschrijving="";
            evenement.evenementID = 0;
            evenement.prijs = 0;
            evenement.startTijd = default(DateTime);
        }

        public void AddToWishlist()
        {
            //wishlist.add(evenement)...
        }
    }
}