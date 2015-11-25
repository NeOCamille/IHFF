﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    public class Evenement
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

        public Evenement()
        {

        }

        public Evenement(int evenementID, string evenementNaam, DateTime startTijd, string beschrijving, double prijs)
        {
            this.evenementID = evenementID;
            this.evenementNaam = evenementNaam;
            this.startTijd = startTijd;
            this.beschrijving = beschrijving;
            this.prijs = prijs;
        }
    }
}