using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    public class Special : Evenement
    {
        [Required]
        public string onderwerp { get; set; }

        [Required]
        public string spreker { get; set; }

        public Special()
        {

        }
        
        public Special(int evenementID, string evenementNaam, DateTime startTijd, string beschrijving, double prijs, string onderwerp, string spreker, int locatieID)
            : base(evenementID, evenementNaam, startTijd, beschrijving, prijs, locatieID)
        {
            this.onderwerp = onderwerp;
            this.spreker = spreker;
        }
    }
}