using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace IHFF_Websystem.Models
{
    public class Medewerker
    {
        [Required]
        public int medewerkerID { get; set; }

        [Required]
        public string gebruikersNaam { get; set; }

        [Required]
        public string passWord { get; set; }

        [Required]
        public string relevantie { get; set; }

        public Medewerker()
        {

        }

        public Medewerker(int medewerkerID, string gebruikersNaam, string passWord, string relevantie)
        {
            this.medewerkerID = medewerkerID;
            this.gebruikersNaam = gebruikersNaam;
            this.passWord = passWord;
            this.relevantie = relevantie;
        }
    }
}