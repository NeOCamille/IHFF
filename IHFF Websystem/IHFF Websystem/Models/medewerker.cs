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

        public int locatieID { get; set; }

        public Medewerker()
        {

        }

        public Medewerker(int medewerkerID, string gebruikersNaam, string passWord, int locatieID)
        {
            this.medewerkerID = medewerkerID;
            this.gebruikersNaam = gebruikersNaam;
            this.passWord = passWord;
            this.locatieID = locatieID;
        }
    }
}