using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IHFF_Websystem.Controllers;

namespace IHFF_Websystem.Models
{
    public class Popup
    {
        //public Evenement evenement { get; set; }

        public int evenementID { get; set; }
        public string evenementNaam { get; set; }
        public string beschrijving { get; set; }
        public double prijs { get; set; }
        public int locatieID { get; set; }

        public string image { get; set; }
        public string locatieNaam { get; set; }
        public string tijd { get; set; }
        public string datum { get; set; }

        public Popup()
        {
           
        }
        public Popup(Evenement e, string locatie)
        {

            evenementNaam = e.evenementNaam;
            beschrijving = e.beschrijving;
            evenementID = e.evenementID;
            prijs = e.prijs;
            locatieNaam = locatie;
            image = e.image;
            datum = String.Format("{0:dd-MM-yyyy}", e.startTijd);
            tijd = String.Format("{0:HH:mm}", e.startTijd);
        }

    }
}