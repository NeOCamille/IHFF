using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    // test comment
    public class Diner
    {
        [Required]
        public int dinerID { get; set; }

        [Required]
        public DateTime startTijd { get; set; }

        [Required]
        public DateTime eindTijd { get; set; }

        [Required]
        public bool foodFilm { get; set; }

        [Required]
        public string opNaamVan { get; set; }

        [Required]
        public double prijs { get; set; }

        //Foreign Key
        public int wishlistID { get; set; }

        //Foreign Key
        public int locatieID { get; set; }

        public Diner()
        {

        }

        public Diner(int dinerID, DateTime startTijd, DateTime eindTijd, bool foodFilm, string opNaamVan, double prijs)
        {
            this.dinerID = dinerID;
            this.startTijd = startTijd;
            this.eindTijd = eindTijd;
            this.foodFilm = foodFilm;
            this.opNaamVan = opNaamVan;
            this.prijs = prijs;
        }
    }
}