using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    public class WishlistPopup
    {
        //evenement
        public int evenementID { get; set; }

        public string evenementNaam { get; set; }

        public DateTime startTijd { get; set; }

        public string beschrijving { get; set; }

        public double prijs { get; set; }

        public int locatieID { get; set; }

        //film
        public string regisseur { get; set; }

        //special
        public string onderwerp { get; set; }

        public string spreker { get; set; }

        //diner
        public int dinerID { get; set; }

        //public DateTime startTijd { get; set; }

        public DateTime eindTijd { get; set; }

        public bool foodFilm { get; set; }

        public string opNaamVan { get; set; }

        //public double prijs { get; set; }

        //Foreign Key
        public int wishlistID { get; set; }

        //Foreign Key
        //public int locatieID { get; set; }

        public int aantal { get; set; }

        //My Discriminator
        public events eventType { get; set; }

        //extra locatie naam
        public string locatieNaam { get; set; }

        //plaatsen
        public int plaatsenVrij { get; set; }
    }
    public enum events
    {
        film,
        special,
        diner
    }
}