using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Gebruikersnaam moet worden ingevuld!")]
        [Display(Name = "Gebruikersnaam")]
        public string gebruikersNaam { get; set; }

        [Required(ErrorMessage = "Wachtwoord moet worden ingevuld!")]
        [Display(Name = "Wachtwoord")]
        public string passWord { get; set; }

        public Login()
        {

        }

        public Login(string gebruikersNaam, string passWord)
        {
            this.gebruikersNaam = gebruikersNaam;
            this.passWord = passWord;
        }
    }
}