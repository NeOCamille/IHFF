using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    public class Film : Evenement
    {
        [Required]
        public string regisseur { get; set; }

        public Film()
        {

        }

        public Film(int evenementID, string evenementNaam, DateTime startTijd, string beschrijving, double prijs, string regisseur)
            : base(evenementID, evenementNaam, startTijd, beschrijving, prijs)
        {
            this.regisseur = regisseur;
        }

    }
}