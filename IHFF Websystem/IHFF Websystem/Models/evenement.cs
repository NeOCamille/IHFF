using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    public abstract class Evenement
    {
        [Required]
        public int evenementID { get; set; }

        [Required]
        public string evenementNaam { get; set; }

        [Required]
        public DateTime startTijd { get; set; }

        [Required]
        public string beschrijving { get; set; }

        [Required]
        public double prijs { get; set; }

        public string Dag { get; set;}

        public int locatieID { get; set; }

        public Evenement()
        {

        }
        public Evenement(int evenementID, string evenementNaam, DateTime startTijd, string beschrijving, double prijs, int locatieID)
        {
            this.evenementID = evenementID;
            this.evenementNaam = evenementNaam;
            this.startTijd = startTijd;
            this.beschrijving = beschrijving;
            this.prijs = prijs;
            this.locatieID = locatieID;
        }
    }
}