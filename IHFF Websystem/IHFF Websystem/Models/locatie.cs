using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    public class Locatie
    {
        [Required]
        public int locatieID { get; set; }

        [Required]
        public string locatieNaam { get; set; }

        [Required]
        public string locatieAdres { get; set; }

        [Required]
        public int maxAantalPlaatsen { get; set; }

        [Required]
        public bool foodFilm { get; set; }

        public string beschrijving { get; set; }

        public string type { get; set; }

        public string image { get; set; }

        public Locatie()
        {

        }

        public Locatie(int locatieID, string locatieNaam, string locatieAdres, int maxAantalPlaatsen, bool foodFilm)
        {
            this.locatieID = locatieID;
            this.locatieNaam = locatieNaam;
            this.locatieAdres = locatieAdres;
            this.maxAantalPlaatsen = maxAantalPlaatsen;
            this.foodFilm = foodFilm;
        }

    }
}